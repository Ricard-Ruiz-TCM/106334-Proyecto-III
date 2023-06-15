using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BuffManager))]
[RequireComponent(typeof(PerkManager))]
[RequireComponent(typeof(SkillManager))]
[RequireComponent(typeof(EquipmentManager))]

public abstract class Actor : BasicActor {

    /** Event action callback, observer, para el uso de skills */
    public static event Action<Node> onSkillUsed;

    /** Callback */
    /** -------- */
    public static event Action onDestinationReached;
    public static event Action<Node> onStepReached;
    public static event Action onStepsAdded;
    /** -------- */

    [SerializeField, Header("Animator")]
    protected Animator _animator;
    public Animator Anim => _animator;

    [Header("readOnly")]
    public int attackRange;

    protected override void Start() {
        base.Start();

        attackRange = _equip.weapon.range;
    }

    #region Movement: 

    /** NavMeshAgent */
    [SerializeField, Header("Movimiento:")]
    protected int _steps;
    protected int _tempSteps;
    protected NavMeshAgent _agent;
    //audio
    private EventInstance footsteps;

    protected List<Node> _route = new List<Node>();
    protected int _stepsDone;

    private void Update() {
        footsteps.set3DAttributes(RuntimeUtils.To3DAttributes(Camera.main.transform.position));
    }

    /** Getters */
    public int maxSteps() {
        return _steps;
    }
    public int stepsDone() {
        return _stepsDone;
    }
    public int stepsRemain() {
        return _tempSteps - _stepsDone;
    }

    /** Add de Steps */
    public void addSteps(int amount) {
        _tempSteps += amount;
        onStepsAdded?.Invoke();
    }

    /** Setters */
    public void setRoute(List<Node> route) {
        _route = route;
    }

    public void UseSkill(Node node) {
        onSkillUsed?.Invoke(node);
    }

    /** Set Destination, método para habilitar el movimiento */
    public void setDestination(List<Node> route) {
        if (route == null)
            endMovement();
        else {
            setRoute(route);
            if (route.Count > 0)
                setStep(route[0]);
        }
    }

    /** Override Move from Turnable */
    public override void move() 
    {

        if (stepReached()) 
        {
            //FMOD PLAY
            PLAYBACK_STATE playbackState;
            footsteps.getPlaybackState(out playbackState);
            Debug.Log("PLAY BACK STATE: " + playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                footsteps.start();
            }
            nextStep();
        }
    }

    /** Override Act from Turnable */
    public override void act() {
    }

    /** Override del endMovement para limpiar Grid */
    public override void endMovement() {
        //FMOD STOP
        footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Stage.StageBuilder.clearGrid();
        //startedStep = true;
        base.endMovement();
    }

    /** Método para ir al siguiente nodo */
    private void nextStep() {
        if (_route == null) 
        {           
            endMovement();
            return;
        }

        if (_stepsDone < _route.Count) {
            onStepReached?.Invoke(Stage.StageBuilder.getGridPlane(_route[_stepsDone]).node);
            _stepsDone++;
            if (_stepsDone >= _route.Count) {
                endMovement();
            } else {
                setStep(_route[_stepsDone]);
            }
        } else {
            endMovement();
            onDestinationReached?.Invoke();
        }
    }


    /** Método para agregar el siguietne step */
    private void setStep(Node node) {
        _agent.SetDestination(Stage.StageBuilder.getGridPlane(node).position);
    }

    /** Comprueba si hemos llegado al punto */
    public bool stepReached() {
        if (!_agent.hasPath && !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathComplete) {
            return true;
        }
        return false;
    }

    #endregion

    /** Override del beginTurn */
    public override void beginTurn() {
        _stepsDone = 0;
        _tempSteps = _steps;
        if (_route != null)
            _route.Clear();
        buffs.applyStartTurnEffect(this);
        skills.updateCooldown();
        buffs.updateBuffs(this);
        Stage.StageBuilder.clearGrid();
        base.beginTurn();
    }

    /** Override del endTurn */
    public override void endTurn() {
        base.endTurn();
        buffs.applyEndTurnEffect(this);
        Stage.StageBuilder.clearGrid();
    }

    /** Override del canAct */
    public override bool canAct() {
        return base.canAct();
    }
    public bool canMoveIfBuff() {
        if (_buffs.isBuffActive(buffsID.Stunned)) {
            return false;
        }
        return true;
    }
    public bool canActIfBuff() {
        if (_buffs.isBuffActive(buffsID.Disarmed) || _buffs.isBuffActive(buffsID.Stunned)) {
            return false;
        }
        return true;
    }

    /** Buffs, Perks & Skill Managers */
    protected BuffManager _buffs;
    public BuffManager buffs => _buffs;
    [SerializeField]
    protected PerkManager _perks;
    public PerkManager perks => _perks;
    [SerializeField]
    protected SkillManager _skills;
    public SkillManager skills => _skills;
    protected EquipmentManager _equip;
    public EquipmentManager equip => _equip;

    /** Base de defensa y armadura */
    /** Depende de equipo y perks, se calcula en Start */
    [SerializeField]
    protected int _baseDamage;
    [SerializeField]
    protected int _baseDefense;

    /** Override para calcular el daño total que podemos hacer */
    public override int totalDamage() {
        int dmg = _baseDamage;

        // BuffModifiers
        foreach (BuffItem bi in _buffs.activeBuffs) {
            if (bi.buff is ModBuff) {
                if (((ModBuff)bi.buff).type.Equals(modType.damage)) {
                    dmg = ((ModBuff)bi.buff).applyMod(dmg);

                }
            }
        }

        return dmg;
    }

    /** Override para calcular la defensa total que podemos hacer */
    public override int totalDefense() {
        int defense = _baseDefense;

        // BuffModifiers
        foreach (BuffItem bi in _buffs.activeBuffs) {
            if (bi.buff is ModBuff) {
                if (((ModBuff)bi.buff).type.Equals(modType.defense)) {
                    defense = ((ModBuff)bi.buff).applyMod(defense);

                }
            }
        }

        return defense;
    }

    /** Override para recibir daño, se tienen en cuenta cositas */
    public override void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {
        // Invencible, kekw
        if (_buffs.isBuffActive(buffsID.Invencible)) {
            return;
        }
        // Arrow Proof y me atacan con arco, kekw
        if ((_equip.weapon.ID.Equals(itemID.Bow)) && (_buffs.isBuffActive(buffsID.ArrowProof))) {
            return;
        }
        // Animator
        Anim.Play("TakeDamage");
        base.takeDamage(from, damage, weapon);
    }

    // Unity Awake
    protected virtual void Awake() {
        // NavMeshAgent
        _agent = GetComponent<NavMeshAgent>();
        // Managers
        _buffs = GetComponent<BuffManager>();
        if (_perks == null)
            _perks = GetComponent<PerkManager>();
        if (_skills == null)
            _skills = GetComponent<SkillManager>();

        _equip = GetComponent<EquipmentManager>();

        _animator = GetComponentInChildren<Animator>();

        //FMOD
        footsteps = FMODManager.instance.CreateEventInstance(FMODEvents.instance.Steps);
    }

    /** Método para construir las skills según las perks y equipamiento */
    public virtual void build() {
        if (_equip.weapon != null) {
            _skills.addSkill(skillID.Attack);
            _baseDamage = _equip.damage;

            GetComponent<WeaponHolder>().setWeapon(_equip.weapon.ID);
        }
        if (_equip.shield != null) {
            _skills.addSkill(skillID.Defense);
            _baseDefense += _equip.shieldDefense;

            GetComponent<WeaponHolder>().setShield();
        }
        if (_equip.armor != null) {
            _baseDefense += _equip.armorDefense;
        }
        if (_equip.weapon != null) {
            _skills.addSkill(equip.weapon.skill);
        }
        foreach (PerkItem pi in _perks.perks) {
            if (pi.perk is SkillPerk) {
                _skills.addSkill(((SkillPerk)pi.perk).skill);
            }
            if (pi.perk is ModPerk) {
                if (pi.perk.modType.Equals(modType.damage)) {
                    _baseDamage = ((ModPerk)pi.perk).apply(_baseDamage);
                }
            }
            if (pi.perk is ModPerk) {
                if (pi.perk.modType.Equals(modType.defense)) {
                    _baseDefense = ((ModPerk)pi.perk).apply(_baseDefense);
                }
            }
            if (pi.perk is ModPerk) {
                if (pi.perk.modType.Equals(modType.movement)) {
                    _steps++;
                }
            }
        }
    }


    /** Método que recupera el último nodo de la ruta */
    public Node getLastRouteNode() {
        if (_route != null) {
            int last = _route.Count - 1;
            return _route[last];
        }
        return null;
    }

}

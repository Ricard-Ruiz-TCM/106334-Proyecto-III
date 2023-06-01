﻿using System;
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
    /** -------- */

    [SerializeField, Header("Animator")]
    protected Animator _animator;
    protected Animator Anim => _animator;

    #region Movement: 

    /** NavMeshAgent */
    [SerializeField, Header("Movimiento:")]
    protected int _steps;
    protected NavMeshAgent _agent;

    protected List<Node> _route = new List<Node>();
    protected int _stepsDone;

    /** Getters */
    public int maxSteps() {
        return _steps;
    }
    public int stepsDone() {
        return _stepsDone;
    }
    public int stepsRemain() {
        return _steps - _stepsDone;
    }

    /** Add de Steps */
    public void addSteps(int amount) {
        _stepsDone -= amount;
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
    public override void move() {
        if (stepReached()) {
            nextStep();
        }
    }

    /** Override Act from Turnable */
    public override void act() {
    }

    /** Override del endMovement para limpiar Grid */
    public override void endMovement() {
        Stage.StageBuilder.clearGrid();
        base.endMovement();
    }

    /** Método para ir al siguiente nodo */
    private void nextStep() {
        if (_route == null) {
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
        return !_agent.hasPath && !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    #endregion

    /** Override del beginTurn */
    public override void beginTurn() {
        _stepsDone = 0;
        if (_route != null)
            _route.Clear();
        buffs.applyStartTurnEffect(this);
        Debug.Log("APPLY TURN EFECTS");
        skills.updateCooldown();
        buffs.updateBuffs(this);
        Debug.Log("UPDATECOOLD");
        Stage.StageBuilder.clearGrid();
        base.beginTurn();
    }

    /** Override del endTurn */
    public override void endTurn() {
        base.endTurn();
        buffs.applyEndTurnEffect(this);
        Debug.Log("APPLY END TURN EFECTS");
        Debug.Log("APPLY update buffs");
        Stage.StageBuilder.clearGrid();
    }

    /** Override del canAct */
    public override bool canAct() {
        return base.canAct();
    }

    /** Buffs, Perks & Skill Managers */
    protected BuffManager _buffs;
    public BuffManager buffs => _buffs;
    [SerializeField]
    protected PerkManager _perks;
    public PerkManager perks => _perks;
    protected SkillManager _skills;
    public SkillManager skills => _skills;
    protected EquipmentManager _equip;
    public EquipmentManager equip => _equip;

    /** Base de defensa y armadura */
    /** Depende de equipo y perks, se calcula en Start */
    protected int _baseDamage;
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
        Anim.SetTrigger("takeDamage");
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
        _skills = GetComponent<SkillManager>();
        _equip = GetComponent<EquipmentManager>();

        _animator = GetComponentInChildren<Animator>();
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

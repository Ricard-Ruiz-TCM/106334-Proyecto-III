using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Actor : BasicActor {

    /** Callback */
    /** -------- */
    public Action onDestinationReached;
    public Action<Node> onStepReached;
    /** -------- */

    #region Movement: 

    /** NavMeshAgent */
    [SerializeField, Header("Movimiento:")]
    protected NavMeshAgent _agent;
    [SerializeField]
    protected List<Node> _route = new List<Node>();

    [SerializeField]
    protected int _maxSteps;
    protected int _stepsDone;

    /** Getters */
    public int maxSteps() {
        return _maxSteps;
    }
    public int stepsDone() {
        return _stepsDone;
    }
    public int stepsRemain() {
        return _maxSteps - _stepsDone;
    }

    /** Add de Steps */
    public void addSteps(int amount) {
        _stepsDone -= amount;
    }

    /** Setters */
    public void setRoute(List<Node> route) {
        _route = route;
    }

    /** Set Destination, método para habilitar el movimiento */
    public void setDestination(List<Node> route) {
        setRoute(route);
        allowMovement();
    }

    /** Override CanMove from Turnable */
    public override bool canMove() {
        return (base.canMove() && _route.Count > 0);
    }

    /** Override Move from Turnable */
    public override void move() {
        if (stepReached()) {
            nextStep();
        }
    }

    /** Método para ir al siguiente nodo */
    private void nextStep() {
        if (_stepsDone < _route.Count) {
            onStepReached?.Invoke(Stage.StageBuilder.GetGridPlane(_route[_stepsDone]).node);
            _stepsDone++;
            if (_stepsDone >= _route.Count) {
                endMovement();
            } else {
                _agent.SetDestination(Stage.StageBuilder.GetGridPlane(_route[_stepsDone]).position);
            }
        } else {
            endMovement();
            onDestinationReached?.Invoke();
        }
    }

    /** Comprueba si hemos llegado al punto */
    public bool stepReached() {
        return !_agent.hasPath && !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    #endregion

    /** Override del onTurn */
    public override void onTurn() {
        
    }

    /** Override del beginTurn */
    public override void beginTurn() {
        _route.Clear();
        _stepsDone = 0;
        base.beginTurn();
    }

    /** Override del endTurn */
    public override void endTurn() {
        base.endTurn();
    }

    /** Override del canAct */
    public override bool canAct() {
        return base.canAct();
    }

    /** Override del act */
    public override void act() {
        
    }

    public override int totalDamage() {
        return 0;
    }

    public override int totalDefense() {
        return 0;
    }

    public override void onActorDeath() {

    }

    public override void takeDamage(int damage, itemID weapon = itemID.NONE) {

        base.takeDamage(damage, weapon);
    }

    /**
    public SOBox<perkID> _perks;
    public SOBox<skillID> _skills;
    public SOBox<buffsID> _buffs;

    #region Equipment:

    [SerializeField, Header("Equipo:")]
    protected ArmorInventoryItem _armor;
    [SerializeField]
    protected WeaponInventoryItem _weapon;
    [SerializeField]
    protected ShieldItem _shield;

    /** Getter de armadura 
    public ArmorItem Armor() {
        return _armor.armor;
    }

    /** Getter de escudo 
    public ShieldItem Shield() {
        return _shield;
    }

    /** Getter de arma 
    public WeaponItem Weapon() {
        return _weapon.weapon;
    }

    #endregion

    protected virtual void Awake() {

        _perks = GetComponent<ActorPerks>();
        _skills = GetComponent<ActorSkills>();
        _status = GetComponent<ActorStatus>();
        _inventory = GetComponent<ActorInventory>();

        _gridMovement = GetComponent<ActorMovement>();
        _gridMovement.onStepReached += (Array2DEditor.nodeType t) => {
            _stepsDone++;
            if (t.Equals(Array2DEditor.nodeType.M)) {
                _stepsDone++;
            } else if (t.Equals(Array2DEditor.nodeType.H)) {
                _stepsDone += 2;
            }
        };
        materialDefault = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
        totalHealth = _health;
    }

    protected void BuildSkills() {
        if (_inventory.Weapon() != null) {
            _skills.AddSkill(skillID.Attack);
        }
        if (_inventory.Shield() != null) {
            _skills.AddSkill(skillID.Defense);
        }
        if (_inventory.Weapon() != null) {
            _skills.AddSkill(_inventory.Weapon().skill);
        }
        foreach (Perk pk in _perks.Perks()) {
            if (pk is SkillPerk) {
                _skills.AddSkill(((SkillPerk)pk).skill);
            }
        }
    }
    protected void AddWeaponToCharacter() {
        //GameObject weaponGameOject = GameObject.Instantiate(_inventory.Weapon().weaponPrefab);
        //weaponGameOject.transform.parent = weaponHolder;
    }

    public int TakeDamage(int damage, itemID weaponItem = itemID.NONE) {
        int newH = Mathf.Max(0, _health - Mathf.Max(0, (damage - Defense())));

        if (weaponItem.Equals(itemID.Bow)) {
            if (Status.isStatusActive(buffsID.ArrowProof)) {
                newH = _health;
            }
        }
        if (_status.isStatusActive(buffsID.Invencible)) {
            newH = _health;
        }

        healtDif = _health - newH;
        _health = newH;


        // Cal Result
        if (_health == 0) {
            _alive = false;
            UnSubscribeManger();
            Die();
        }

        return healtDif;
    }

    public override int TotalDamage() {
        throw new System.NotImplementedException();
    }

    public override int TotalDefense() {
        throw new System.NotImplementedException();
    }


    public override void Act() {
        throw new System.NotImplementedException();
    }

    public int Damage() {
        int dmg = 0;

        // Get Values
        dmg += _inventory.Weapon().damage[0];

        // Apply Modifiers
        foreach (Perk pk in _perks.Perks()) {
            if (pk is ModPerk) {
                if (pk.modType.Equals(modType.damage)) {
                    dmg += (int)((ModPerk)pk).value;
                }
            }
        }

        // check Status
        foreach (BuffItem sitem in _status.ActiveStatus) {
            if (sitem.buff is ModBuff) {
                if (((ModBuff)sitem.buff).type.Equals(modType.damage)) {
                    switch (((ModBuff)sitem.buff).operation) {
                        case modOperation.add:
                            dmg += ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.sub:
                            dmg -= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.mult:
                            dmg *= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.div:
                            dmg /= ((ModBuff)sitem.buff).value;
                            break;
                    }

                }
            }
        }

        return dmg;
    }

    public int Defense() {
        int defense = 0;

        // Get Values
        if (_inventory.Armor() != null)
            defense = _inventory.Armor().defense[0];

        // Apply Modifiers
        foreach (Perk pk in _perks.Perks()) {
            if (pk is ModPerk) {
                if (pk.modType.Equals(modType.defense)) {
                    defense += (int)((ModPerk)pk).value;
                }
            }
        }

        // check Status
        foreach (BuffItem sitem in _status.ActiveStatus) {
            if (sitem.buff is ModBuff) {
                if (((ModBuff)sitem.buff).type.Equals(modType.defense)) {
                    switch (((ModBuff)sitem.buff).operation) {
                        case modOperation.add:
                            defense += ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.sub:
                            defense -= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.mult:
                            defense *= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.div:
                            defense /= ((ModBuff)sitem.buff).value;
                            break;
                    }
                }
            }
        }

        return defense;
    }


    public override void onActorDeath() {
        throw new NotImplementedException();
    }
    */

}

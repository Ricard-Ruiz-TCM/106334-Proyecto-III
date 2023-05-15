﻿using UnityEngine;

public abstract class Actor : ActorManager {

    protected ActorPerks _perks;
    protected ActorSkills _skills;
    protected ActorStatus _status;
    public ActorStatus Status => _status;
    protected ActorInventory _inventory;
    public WeaponItem Weapon => _inventory.Weapon();

    public bool CanBePlaced = false;

    // Grid Movement
    protected GridMovement _gridMovement;
    public GridMovement GridM() {
        return _gridMovement;
    }

    [SerializeField, Header("Core:")]
    protected int _health;
    protected int totalHealth;
    [SerializeField]
    protected bool _isAlive;
    public bool IsAlive() {
        return _isAlive;
    }

    [SerializeField, Header("Movement:")]
    protected int _movement;
    protected int _movementsDone;

    [SerializeField, Header("CanMove:")]
    public bool canMove = true;

    [SerializeField, Header("CanInteract:")]
    public bool canInteract = true;

    [SerializeField, Header("Stun:")]
    protected int _isStuned = 0;
    protected int _stunedRounds = 1;

    [SerializeField, Header("Turtle:")]
    protected bool _isTurtle = false;

    [SerializeField, Header("WeaponPoint:")]
    protected Transform weaponHolder;

    int damageModRounds = 0;
    int defenseModRounds = 0;
    int anteriorDefense = 0;
    int anteriorDamage = 0;
    int invisibleRoundsController;

    [SerializeField] Material materialInvisible;
    Material materialDefault;

    int healtDif = 0;


    protected virtual void Awake() {

        _perks = GetComponent<ActorPerks>();
        _skills = GetComponent<ActorSkills>();
        _status = GetComponent<ActorStatus>();
        _inventory = GetComponent<ActorInventory>();

        _gridMovement = GetComponent<GridMovement>();
        _gridMovement.onStepReached += (Array2DEditor.nodeType t) => {
            _movementsDone++;
            if (t.Equals(Array2DEditor.nodeType.M)) {
                _movementsDone++;
            } else if (t.Equals(Array2DEditor.nodeType.H)) {
                _movementsDone += 2;
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
    protected void AddWeaponToCharacter()
    {
        //GameObject weaponGameOject = GameObject.Instantiate(_inventory.Weapon().weaponPrefab);
        //weaponGameOject.transform.parent = weaponHolder;
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

    public int GetHealth() {
        return _health;
    }
    public int MaxHealth() {
        return totalHealth;
    }
    public void SetHealth(int value) {
        _health = value;
    }
    public int GetTotalHealthPercentage() {
        return ((_health / totalHealth) * 100);
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

    public void ModMoves(int amount) {
        _movementsDone += amount;
    }

    public virtual int Movement() {
        int movement = _movement;

        movement -= _movementsDone;

        return movement;
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
            _isAlive = false;
            UnSubscribeManger();
            Die();
        }

        return healtDif;
    }
    public int DamageTaken() {
        return healtDif;
    }

    public abstract void Die();

    public progress moving {
        get; private set;
    }
    public progress acting {
        get; private set;
    }
    public bool hasTurnEnded {
        get; private set;
    }

    /** Métodos para entrar/salir al sistema de turnos */
    protected void SubscribeManager() {
        TurnManager.instance.Subscribe(this);
        CombatManager.instance.Subscribe(this);
    }
    protected void UnSubscribeManger() {
        TurnManager.instance.Unsubscribe(this);
        CombatManager.instance.Unsubscribe(this);
    }

    /** Métodos de control del turno **/
    public virtual void BeginTurn() {
        moving = progress.ready;
        acting = progress.ready;
        _movementsDone = 0;
        hasTurnEnded = false;

        /** Effects of the Status */
        _status.StatusEffect();

    }

    public void EndTurn() {
        hasTurnEnded = true;
        /** Update the duration of the Status */
        _status.UpdateStatus();
        _skills.UpdateSkillCooldown();
    }

    /** Método de control de Acción */
    public abstract bool CanAct();
    public virtual void Act() {
        StartAct();
    }
    protected void StartAct() {
        acting = progress.doing;
    }
    public void EndAction() {
        acting = progress.done;
    }

    /** Método de control de Movimiento */
    public abstract bool CanMove();
    public virtual void Move() {
        StartMove();
    }
    protected void StartMove() {
        moving = progress.doing;
    }
    public virtual void EndMovement() {
        moving = progress.done;
    }

}
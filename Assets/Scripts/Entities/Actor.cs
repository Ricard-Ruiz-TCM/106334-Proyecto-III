using System.Collections.Generic;
using UnityEngine;

public abstract class ActorManager : MonoBehaviour {

    // Actor getter
    private Actor _actor = null;
    [HideInInspector]
    public Actor actor {
        get {
            if (_actor == null)
                _actor = GetComponent<Actor>();

            return _actor;
        }
        set {
        }
    }

}

public abstract class Actor : ActorManager {

    protected ActorPerks _perks;
    protected ActorSkills _skills;
    protected ActorStatus _status;
    protected ActorInventory _inventory;

    public bool CanBePlaced = false;

    // Grid Movement
    protected GridMovement _gridMovement;
    public GridMovement GridM() {
        return _gridMovement;
    }

    [SerializeField, Header("Core:")]
    protected int _health;
    protected int _tempDef;
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

    [SerializeField] Material materialInvisible;
    Material materialDefault;


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
    }

    protected void BuildSkills() {
        if (_inventory.Weapon() != null) {
            _skills.AddSkill(skills.Attack);
        }
        if (_inventory.Shield() != null) {
            _skills.AddSkill(skills.Defense);
        }
        if (_inventory.Weapon() != null) {
            _skills.AddSkill(_inventory.Weapon().skill.skill);
        }
        foreach (Perk pk in _perks.Perks()) {
            if (pk is SkillPerk) {
                _skills.AddSkill(((SkillPerk)pk)._skill.skill);
            }
        }
    }

    public int Damage() {
        int dmg = 0;

        // Get Values
        dmg += _inventory.Weapon().damage[0];

        // Apply Modifiers
        foreach (Perk pk in _perks.Perks()) {
            if (pk is ModPerk) {
                if (pk.modificationType.Equals(perkModification.damage)) {
                    dmg += (int)((ModPerk)pk).modifier;
                }
            }
        }

        return dmg;
    }

    public int Health() {
        return _health;
    }

    public int Defense() {
        int defense = 0;

        // Get Values
        if (_inventory.Armor() != null)
            defense = _inventory.Armor().defense[0];

        // Apply Modifiers
        foreach (Perk pk in _perks.Perks()) {
            if (pk is ModPerk) {
                if (pk.modificationType.Equals(perkModification.defense)) {
                    defense += (int)((ModPerk)pk).modifier;
                }
            }
        }

        defense += _tempDef;

        return defense;
    }

    public virtual int Movement() {
        int movement = _movement;

        movement -= _movementsDone;

        return movement;
    }

    public void TakeDamage(int damage) {

        _health = Mathf.Max(0, _health -= Mathf.Max(0, (damage - Defense())));

        // Cal Result
        if (_health == 0) {
            _isAlive = false;
            UnSubscribeManger();
            GameObject.Destroy(this.gameObject);
        }
    }

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
        _tempDef = 0;
        hasTurnEnded = false;

        /** Custom Status EFFECTS */
        // Invisible
        if (_status.isStatusActive(aStatus.Invisible)) {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = materialDefault;
        }
        // Stunned
        if (_status.isStatusActive(aStatus.Stunned)) {
            moving = progress.done;
            acting = progress.done;
        }

    }
    public void EndTurn() {
        hasTurnEnded = true;
        /** Effects of the Status */
        _status.StatusEffect();
        /** Update the duration of the Status */
        _status.UpdateStatus();
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
    protected void EndMovement() {
        moving = progress.done;
    }

}
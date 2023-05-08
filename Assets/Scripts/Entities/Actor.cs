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

    [SerializeField, Header("CanInteract:")]
    public bool canInteract = true;

    [SerializeField, Header("Invisible:")]
    protected bool isInvisible = false;
    public bool IsInvisible() {
        return isInvisible;
    }
    protected int invisibleRounds = 1;

    [SerializeField, Header("Stun:")]
    protected int _isStuned = 0;
    protected int _stunedRounds = 1;

    [SerializeField, Header("Turtle:")]
    protected bool _isTurtle = false;

    int damageModRounds = 0;
    int defenseModRounds = 0;
    int anteriorDefense = 0;
    int anteriorDamage = 0;
    int invisibleRoundsController;

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
                _skills.AddSkill(((SkillPerk)pk).skill.skill);
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
                if (pk.modificationType.Equals(modificationType.damage)) {
                    dmg += (int)((ModPerk)pk).modifier;
                }
            }
        }

        // check Status
        foreach (StatusItem sitem in _status.ActiveStatus) {
            if (sitem.status is ModStatus) {
                if (((ModStatus)sitem.status).type.Equals(modificationType.damage)){
                    dmg *= ((ModStatus)sitem.status).modification;
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
                if (pk.modificationType.Equals(modificationType.defense)) {
                    defense += (int)((ModPerk)pk).modifier;
                }
            }
        }

        // check Status
        foreach (StatusItem sitem in _status.ActiveStatus) {
            if (sitem.status is ModStatus) {
                if (((ModStatus)sitem.status).type.Equals(modificationType.defense)) {
                    defense *= ((ModStatus)sitem.status).modification;
                }
            }
        }

        defense += _tempDef;

        return defense;
    }
    public void UpdateDefense(int number, string equation, int roundsToLast) {
        /**Expertise ex = _upgrades.Find(x => x._item.Equals(_armor._item));
        if (anteriorDefense == 0) {
            anteriorDefense = _armor._defense[(ex != null ? ex._level : 0)];
        }

        switch (equation) {
            case "+":
                _armor._defense[(ex != null ? ex._level : 0)] += number;
                break;
            case "-":
                _armor._defense[(ex != null ? ex._level : 0)] -= number;
                break;
            case "/":
                _armor._defense[(ex != null ? ex._level : 0)] /= number;
                break;
            case "*":
                _armor._defense[(ex != null ? ex._level : 0)] *= number;
                break;
        }
        defenseModRounds = roundsToLast;*/
    }
    public void UpdateAttack(int number, string equation, int roundsToLast) {
        /*Expertise ex = _upgrades.Find(x => x._item.Equals(_weapon._item));
        if (anteriorDamage == 0) {
            anteriorDamage = _weapon._damage[(ex != null ? ex._level : 0)];
        }

        switch (equation) {
            case "+":
                _weapon._damage[(ex != null ? ex._level : 0)] += number;
                break;
            case "-":
                _weapon._damage[(ex != null ? ex._level : 0)] -= number;
                break;
            case "/":
                _weapon._damage[(ex != null ? ex._level : 0)] /= number;
                break;
            case "*":
                _weapon._damage[(ex != null ? ex._level : 0)] *= number;
                break;
        }
        damageModRounds = roundsToLast;*/
    }

    public virtual int Movement() {
        int movement = _movement;

        movement -= _movementsDone;

        return movement;
    }

    public void TakeDamage(int damage, items weaponItem = items.NONE) {

        int newH = Mathf.Max(0, _health -= Mathf.Max(0, (damage - Defense())));


        if (weaponItem.Equals(items.Bow)) {
            if (Status.isStatusActive(buffsnDebuffs.ArrowInmune)){
                newH = _health;
            }
        }

        _health = newH;


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

        /** Effects of the Status */
        _status.StatusEffect();

        _isTurtle = false;

        if (_isStuned > 0) {
            moving = progress.done;
            acting = progress.done;
        }

        if (damageModRounds != 0) {
            damageModRounds--;
            if (damageModRounds == 0) {
                //Expertise ex = _upgrades.Find(x => x._item.Equals(_weapon._item));
                //_weapon._damage[(ex != null ? ex._level : 0)] = anteriorDamage;
            }
        }
        if (defenseModRounds != 0) {
            defenseModRounds--;
            if (defenseModRounds == 0) {
                //Expertise ex = _upgrades.Find(x => x._item.Equals(_armor._item));
                //_armor._defense[(ex != null ? ex._level : 0)] = anteriorDefense;
            }
        }

    }
    public void EndTurn() {
        hasTurnEnded = true;
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
    public void EndMovement() {
        moving = progress.done;
    }

}
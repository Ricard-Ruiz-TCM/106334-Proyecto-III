using UnityEngine;
using System.Collections.Generic;

public abstract class Actor : MonoBehaviour, ITurnable {

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
    public bool IsAlive() { return _isAlive; }

    [SerializeField, Header("Movement:")]
    protected int _movement;
    protected int _movementsDone;

    [SerializeField, Header("Equipment:")]
    protected ArmorItem _armor;
    [SerializeField]
    protected WeaponItem _weapon;
    [SerializeField]
    protected ArmorItem _shield;

    [SerializeField]
    protected List<Expertise> _upgrades;

    [SerializeField, Header("Inventory:")]
    protected Inventory _inventory;

    [SerializeField, Header("Acive Perks:")]
    protected List<Perk> _perks;

    [SerializeField, Header("Skills avaliable:")]
    protected List<SkillItem> _skills;

    [SerializeField, Header("CanMove:")]
    public bool canMove = true;

    [SerializeField, Header("Invisible:")]
    protected bool isInvisible = false;
    protected int invisibleRounds = 1;
    int invisibleRoundsController;
    [SerializeField] Material materialInvisible;
    Material materialDefault;


    protected virtual void Awake() {
        _gridMovement = GetComponent<GridMovement>();
        _gridMovement.onStepReached += () => { _movementsDone++; };
        invisibleRoundsController = invisibleRounds;
        materialDefault = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material; 
    }

    protected void BuildSkills() {
        if (_weapon != null) {
            AddSkill(_weapon._skill._skill);
        }
        if (_shield != null) {
            //AddSkill(_shield._defense)
        }
        foreach (Perk pk in _perks) {
            if (pk is SkillPerk)
                AddSkill(((SkillPerk)pk)._skill._skill);
        }
    }

    public int Damage() {
        int dmg = 0;

        // Get Values
        Expertise ex = _upgrades.Find(x => x._item.Equals(_weapon._item));
        dmg += _weapon._damage[(ex != null ? ex._level : 0)];

        // Apply Modifiers
        foreach (Perk pk in _perks) {
            if (pk is ModPerk) {
                if (pk._modificationType.Equals(perkModification.damage)) {
                    dmg += ((ModPerk)pk)._modifier;
                }
            }
        }

        return dmg;
    }

    public int Health() {
        return _health;
    }

    public void AddTempDef(int def) {
        _tempDef = def;
    }

    public void SetInvisible(bool inv)
    {
        isInvisible = inv;
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = materialInvisible;
    }
    public int Defense() {
        int defense = 0;

        // Get Values
        Expertise ex = _upgrades.Find(x => x._item.Equals(_armor._item));
        defense = _armor._defense[(ex != null ? ex._level : 0)];

        // Apply Modifiers
        foreach (Perk pk in _perks) {
            if (pk is ModPerk) {
                if (pk._modificationType.Equals(perkModification.armor)) {
                    defense += ((ModPerk)pk)._modifier;
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
        }
    }

    public void AddPerk(perks perk) {

    }
    public void RemovePerk(perks perk) {

    }
    public bool HavePerk(perks perk) {
        foreach(Perk pks in _perks) {
            if (pks._perk.Equals(perk))
                return true;
        }
        return false;
    }

    public void AddSkill(skills skill) {
        bool already = false;

        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill._skill.Equals(skill))
                already = true;
        }

        if (!already) {
            Skill sk = uCore.GameManager.GetSkill(skill);
            _skills.Add(new SkillItem() { skill = sk, cooldown = sk._cooldown }); ;
        }
    }
    public void RemoveSkill(skills skill) {
        if (HaveSkill(skill)) {
            int pos = -1;
            for(int i = 0; i < _skills.Count; i++) {
                if (_skills[i].skill.Equals(skill)) {
                    pos = i;
                    break;
                }
            }
            if (pos != -1) {
                _skills.RemoveAt(pos);
            }
        }
    }
    public void UseSkill(skills skill) {
        foreach(SkillItem skillItem in _skills) {
            if (skillItem.skill._skill.Equals(skill)) {
                if (skillItem.cooldown <= 0) {
                    skillItem.skill.Special(this);
                    skillItem.cooldown = skillItem.skill._cooldown;
                }
            }
        }
    }
    public void UpdateSkillCooldown() {
        foreach (SkillItem skillItem in _skills) {
            if (skillItem.cooldown > 0) {
                skillItem.cooldown--;
            }
        }
    }
    public bool HaveSkill(skills skill) {
        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill._skill.Equals(skill))
                return true;
        }
        return false;
    }

    public List<SkillItem> Skills() {
        return _skills;
    }

    #region ITurnable 
    public Actor actor {
        get { return this; } private set { }
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
        FindObjectOfType<TurnManager>().Add(this);
        FindObjectOfType<CombatManager>().Add(this);
    }
    protected void UnSubscribeManger() {
        FindObjectOfType<TurnManager>().Remove(this);
    }

    /** Métodos de control del turno **/
    public void BeginTurn() {
        moving = progress.ready;
        acting = progress.ready;
        _movementsDone = 0;
        _tempDef = 0;
        hasTurnEnded = false;
        if(invisibleRoundsController > 0 && isInvisible)
        {
            invisibleRoundsController--;
        }
        else if (isInvisible)
        {
            invisibleRoundsController = invisibleRounds;
            isInvisible = false;
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = materialDefault;
        }

    }
    public void EndTurn() {
        hasTurnEnded = true;
        UpdateSkillCooldown();
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
    #endregion

}
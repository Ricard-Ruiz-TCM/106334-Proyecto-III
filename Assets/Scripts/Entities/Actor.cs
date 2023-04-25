using UnityEngine;
using System.Collections.Generic;

public abstract class Actor : MonoBehaviour, ITurnable {

    [SerializeField, Header("Core:")]
    protected int _health;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected bool _isAlive;
    public bool IsAlive() { return _isAlive; }

    [SerializeField, Header("Movement:")]
    protected int _movement;
    public int Movement() { return _movement; }

    [SerializeField, Header("Equipment:")]
    protected ArmorItem _armor;
    [SerializeField]
    protected WeaponItem _weapon;
    [SerializeField]
    protected ArmorItem _shield;

    [SerializeField, Header("Inventory:")]
    protected Inventory _inventory;

    [SerializeField, Header("Acive Perks:")]
    protected List<Perk> _perks;

    [SerializeField, Header("Skills avaliable:")]
    protected List<SkillItem> _skills;

    public int Damage() {
        int dmg = 0;

        // Get Values
        dmg += _weapon._damage[0];

        // Apply Modifiers
        

        return dmg;
    }
    public void TakeDamage(int damage) {
        int defense = 0, health = 0;

        // Get Values
        defense = _armor._defense[0];
        health = _health;

        // Apply Modifiers
        health = Mathf.Max(0, health -= Mathf.Max(0, (damage - defense)));

        // Cal Result
        _health = health;
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
                    skillItem.skill.Special();
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
    }
    protected void UnSubscribeManger() {
        FindObjectOfType<TurnManager>().Remove(this);
    }

    /** Métodos de control del turno **/
    public void BeginTurn() {
        moving = progress.ready;
        acting = progress.ready;
        hasTurnEnded = false;
    }
    public void EndTurn() {
        hasTurnEnded = true;
    }

    /** Método de control de Acción */
    public abstract bool CanAct();
    public virtual void Act() {
        StartAct();
    }
    protected void StartAct() {
        acting = progress.doing;
    }
    protected void EndAction() {
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
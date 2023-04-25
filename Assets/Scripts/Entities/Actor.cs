using UnityEngine;
using System.Collections.Generic;

public abstract class Actor : MonoBehaviour, ITurnable {

    [SerializeField, Header("Statistics:")]
    protected Statistics _statistics;

    [SerializeField, Header("Equipment:")]
    protected ArmorUpgradeItem _armor;
    [SerializeField]
    protected WeaponUpgradeItem _weapon;

    [SerializeField, Header("Inventory:")]
    protected Inventory _inventory;

    public List<Perks> _perks;

    public List<SkillItem> _skills;
    

    public void TakeDamage(int damage) {
        _statistics.TakeDamage(damage);
    }

    public float Damage() {
        float dmg = 0;

        dmg += _weapon.Damage();

        return dmg;
    }

    public void AddSkill(Skill sk) {
        bool already = false;

        foreach (SkillItem skI in _skills) {
            if (skI.skill.Equals(sk))
                already = true;
        }

        if (!already)
            _skills.Add(new SkillItem() { skill = sk, cooldown = sk._cooldown }); ;
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
            if (pos != -1)
                _skills.RemoveAt(pos);
        }
    }
    public void UseSkill(skills skill) {
        foreach(SkillItem skI in _skills) {
            if (skI.skill.Equals(skill)) {
                if (skI.cooldown <= 0) {
                    skI.skill.Special();
                    skI.cooldown = skI.skill._cooldown;
                }
            }
        }
    }
    public void UpdateSkillCooldown() {
        foreach (SkillItem skI in _skills) {
            if (skI.cooldown > 0) {
                skI.cooldown--;
            }
        }
    }
    public bool HaveSkill(skills skill) {
        foreach (SkillItem skI in _skills) {
            if (skI.skill.Equals(skill))
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
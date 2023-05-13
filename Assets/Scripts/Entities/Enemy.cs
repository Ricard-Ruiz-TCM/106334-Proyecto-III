using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement)), RequireComponent(typeof(ActorPerks))]
[RequireComponent(typeof(ActorSkills)), RequireComponent(typeof(ActorStatus))]
[RequireComponent(typeof(ActorInventory))]
public class Enemy : Actor {

    // Actor target de skills y ataques, etc.
    private Actor _target;

    [SerializeField, Header("Clase:")]
    private enemyClass _class;

    [SerializeField, Header("IA:")]
    private enemyCombatStyle _combatStyle;
    private List<skills> normalSkillOrder;
    private List<skills> lowHpSkillOrder;

    // Unity Awake
    protected override void Awake() {
        base.Awake();
        normalSkillOrder = new List<skills>();
        lowHpSkillOrder = new List<skills>();

        foreach (skills value in skills.GetValues(typeof(skills))) {
            if (value.Equals(skills.NONE) || value.Equals(skills.MAX)) {
                continue;
            }
            normalSkillOrder.Add(value);
        }
        for (int i = normalSkillOrder.Count - 1; i > 0; i--) {
            lowHpSkillOrder.Add(normalSkillOrder[i]);
        }
    }

    // Unity Start
    void Start() {
        SubscribeManager();
        BuildSkills();

        transform.position = Stage.StageManager.RandomPosition();
    }

    public override bool CanAct() {
        return acting.Equals(progress.ready) && moving.Equals(progress.done);
    }
    public override void Act() {
        base.Act();

        if (_target != null) {
            if (_inventory.Weapon() != null) 
            {
                if (GetTotalHealthPercentage() > 20)
                {
                    for (int i = 0; i < normalSkillOrder.Count; i++)
                    {
                        Skill usingSkill = _skills.ReturnSkill(normalSkillOrder[i]);
                        if(usingSkill != null)
                        {
                            if (Stage.StageManager.InRange(transform, _target, usingSkill.range, usingSkill.needRangeToUse))
                            {                                
                                _skills.UseSkill(normalSkillOrder[i]);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < lowHpSkillOrder.Count; i++)
                    {
                        Skill usingSkill = _skills.ReturnSkill(normalSkillOrder[i]);
                        if (Stage.StageManager.InRange(transform, _target, usingSkill.range, usingSkill.needRangeToUse))
                        {
                            _skills.UseSkill(normalSkillOrder[i]);
                        }
                    }
                }
            }
        }
        EndAction();
    }

    public override bool CanMove() {
        return moving.Equals(progress.ready) && !acting.Equals(progress.doing) && canMove;
    }

    public override void Move() {
        base.Move();
        if (_target == null) 
        {
            Debug.Log("DASD");
            EndMovement();
            return;
        }

        GridPlane targetPlane = Stage.StageBuilder.GetGridPlane(_target.transform.position);
        GridPlane myPlane = Stage.StageBuilder.GetGridPlane(transform.position);
        GridPlane destiny = myPlane;
        if (GetTotalHealthPercentage() > 20)
        {
            if (_combatStyle.Equals(enemyCombatStyle.melee))
            {
                destiny = Stage.StageBuilder.FindClosesGridPlaneTo(targetPlane, myPlane);
            }
            else
            {
                List<Node> route = _gridMovement.CalcRoute(transform.position, targetPlane);
                route.Reverse();
                if (route.Count > _inventory.Weapon().range)
                {
                    destiny = Stage.StageBuilder.GetGridPlane(route[_inventory.Weapon().range]);
                }
            }
        }
        
        _gridMovement.SetDestination(transform.position, destiny, Movement());
        _gridMovement.onDestinationReached += EndMovement;
    }

    public override void BeginTurn() {
        base.BeginTurn();
        _target = Stage.StageManager.FindNearestPlayers(transform);
    }

    public override void Die() {
        transform.position = new Vector3(10000, 10000, 10000);
    }
}
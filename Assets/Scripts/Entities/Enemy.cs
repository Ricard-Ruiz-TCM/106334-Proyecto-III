using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement)), RequireComponent(typeof(ActorPerks))]
[RequireComponent(typeof(ActorSkills)), RequireComponent(typeof(ActorStatus))]
[RequireComponent(typeof(ActorInventory))]
public class Enemy : Actor {

    public Actor _target;

    [SerializeField]
    private enemyCombatStyle _combatStyle;

    // Unity Awake
    protected override void Awake() {
        base.Awake();
    }

    // Unity Start
    void Start() {
        SubscribeManager();
        /**BuildSkills();

        if (_weapon.item.Equals(items.Bow)) {
            _combatStyle = enemyCombatStyle.ranged;
        } else {
            _combatStyle = enemyCombatStyle.melee;
        }*/

    }

    public override bool CanAct() {
        return acting.Equals(progress.ready) && moving.Equals(progress.done);
    }
    public override void Act() {
        base.Act();

        if (_target != null) {
            if (_inventory.Weapon() != null) {
                // Weapon Básica, el ataque
                if (InRange(_target, _inventory.Weapon().range)) {
                    _target.TakeDamage(Damage());
                    //if (InRange(_target, _weapon._range)) {
                    //    _target.TakeDamage(Damage());
                    //}
                    /*if (InRange(_target, Skills()[1].skill._range))
                    {
                        Debug.Log("hola");
                        UseSkill(Skills()[1].skill._skill);
                        //_target.TakeDamage(Damage());
                    }*/
                }
            }
            EndAction();
        }
    }

    public override bool CanMove() {
        return moving.Equals(progress.ready) && !acting.Equals(progress.doing) && canMove;
    }
    public override void Move() {
        base.Move();
        if (_target == null) {
            EndMovement();
            return;
        }
        GridPlane targetPlane = Stage.StageBuilder.GetGridPlane(_target.transform.position);
        GridPlane myPlane = Stage.StageBuilder.GetGridPlane(transform.position);
        GridPlane destiny = myPlane;
        if (_combatStyle.Equals(enemyCombatStyle.melee)) {
            destiny = Stage.StageBuilder.FindClosesGridPlaneTo(targetPlane, myPlane);
        } else {
            List<Node> route = _gridMovement.CalcRoute(transform.position, targetPlane);
            route.Reverse();
            if (route.Count > _inventory.Weapon().range) {
                destiny = Stage.StageBuilder.GetGridPlane(route[_inventory.Weapon().range]);
            }

        }
        _gridMovement.SetDestination(transform.position, destiny, Movement());
        _gridMovement.onDestinationReached += EndMovement;
    }


    public Actor FindNearest() {

        Actor actor = null;
        float dist = Mathf.Infinity;

        foreach (Actor obj in CombatManager.instance.FindPlayers()) {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance < dist) {
                if (obj.Status.isStatusActive(buffsnDebuffs.Invisible)) 
                {
                    actor = obj;
                    dist = distance;
                }
            }
        }

        return actor;
    }

    public bool InRange(Actor target, int range) {
        GridPlane targetPlane = Stage.StageBuilder.GetGridPlane(target.transform.position);
        GridPlane myPlane = Stage.StageBuilder.GetGridPlane(transform.position);
        return Stage.StageBuilder.GetGridDistanceBetween(myPlane, targetPlane) <= range;
    }


    public override void BeginTurn() {
        base.BeginTurn();

        _target = FindNearest();

    }


}
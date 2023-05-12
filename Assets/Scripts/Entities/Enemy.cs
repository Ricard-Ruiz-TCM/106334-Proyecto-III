using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement)), RequireComponent(typeof(ActorPerks))]
[RequireComponent(typeof(ActorSkills)), RequireComponent(typeof(ActorStatus))]
[RequireComponent(typeof(ActorInventory))]
public class Enemy : Actor {

    // Actor target de skills y ataques, etc.
    private Actor _target;

    [SerializeField, Header("IA:")]
    private enemyCombatStyle _combatStyle;

    // Unity Awake
    protected override void Awake() {
        base.Awake();
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
            if (_inventory.Weapon() != null) {
                // Weapon Básica, el ataque
                if (Stage.StageManager.InRange(transform, _target, _inventory.Weapon().range)) {
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

    public override void BeginTurn() {
        base.BeginTurn();

        _target = Stage.StageManager.FindNearestPlayers(transform);

    }


}
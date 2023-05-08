﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Enemy : Actor {

    public CombatManager _Cmanager;

    public Actor _target;

    [SerializeField]
    private enemyCombatStyle _combatStyle;

    // Unity Awake
    protected override void Awake() {
        base.Awake();

        _Cmanager = GameObject.FindObjectOfType<CombatManager>();
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

    public override bool CanAct() { return acting.Equals(progress.ready) && moving.Equals(progress.done); }
    public override void Act() {
        base.Act();

        if (_target != null) {
            if (_inventory.Weapon() != null) {
                // Weapon Básica, el ataque
                if (InRange(_target, _inventory.Weapon().range)) {
                    _target.TakeDamage(Damage());
                }
            }
        }
        EndAction();
    }

    public override bool CanMove() { return moving.Equals(progress.ready) && !acting.Equals(progress.doing) && canMove; }
    public override void Move() {
        base.Move();
        if (_target == null) {
            EndMovement();
            return;
        }
        GridPlane targetPlane = _gridMovement.Builder().GetGridPlane(_target.transform.position);
        GridPlane myPlane = _gridMovement.Builder().GetGridPlane(transform.position);
        GridPlane destiny = myPlane;
        if (_combatStyle.Equals(enemyCombatStyle.melee)) {
            destiny = _gridMovement.Builder().FindClosesGridPlaneTo(targetPlane, myPlane);
        } else {
            List<Node> route = _gridMovement.CalcRoute(transform.position, targetPlane);
            route.Reverse();
            if (route.Count > _inventory.Weapon().range) {
                destiny = _gridMovement.Builder().GetGridPlane(route[_inventory.Weapon().range]);
            }

        }
        _gridMovement.SetDestination(transform.position, destiny, Movement());
        _gridMovement.onDestinationReached += EndMovement;
    }


    public Actor FindNearest() {

        Actor actor = null;
        float dist = Mathf.Infinity;

        foreach (Actor obj in _Cmanager.FindPlayers()) {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance < dist) {
                if (!obj.GetComponent<ActorStatus>().isStatusActive(aStatus.Invisible)) {
                    actor = obj; dist = distance;
                }
            }
        }

        return actor;
    }

    public bool InRange(Actor target, int range) {
        GridPlane targetPlane = _gridMovement.Builder().GetGridPlane(target.transform.position);
        GridPlane myPlane = _gridMovement.Builder().GetGridPlane(transform.position);
        return _gridMovement.Builder().GetGridDistanceBetween(myPlane, targetPlane) <= range;
    }


    public override void BeginTurn() {
        base.BeginTurn();

        _target = FindNearest();

    }


}
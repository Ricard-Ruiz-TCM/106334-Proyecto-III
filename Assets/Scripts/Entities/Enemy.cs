﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorMovement)), RequireComponent(typeof(ActorPerks))]
[RequireComponent(typeof(ActorSkills)), RequireComponent(typeof(ActorStatus))]
[RequireComponent(typeof(ActorInventory))]
public class Enemy : Turnable {

    // Actor target de skills y ataques, etc.
    private Turnable _target;

    [SerializeField, Header("Clase:")]
    private enemyClass _class;

    [SerializeField, Header("IA:")]
    private enemyCombatStyle _combatStyle;
    private List<skillID> normalSkillOrder;
    private List<skillID> lowHpSkillOrder;

    // Unity Awake
    protected override void Awake() {
        base.Awake();
        normalSkillOrder = new List<skillID>();
        lowHpSkillOrder = new List<skillID>();

        foreach (skillID value in skillID.GetValues(typeof(skillID))) {
            if (value.Equals(skillID.NONE) || value.Equals(skillID.MAX)) {
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
        AddWeaponToCharacter();
    }

    public override bool CanAct() {
        return acting.Equals(progress.ready) && moving.Equals(progress.done);
    }
    public override void Act() {
        base.Act();
        if (_target != null) {
            if (_inventory.Weapon() != null) {
                if (Stage.StageManager.InRange(transform, _target, _inventory.Weapon().range)) {
                    _target.TakeDamage(Damage());
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

        _target = Stage.StageManager.FindNearestPlayer(transform);
    }

    public override void Die() {
        transform.position = new Vector3(10000, 10000, 10000);
    }

}
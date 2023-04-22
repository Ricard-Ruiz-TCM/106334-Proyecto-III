using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GridMovement))]
public class Player : Actor {

    // Grid Movement
    private GridMovement _movement;

    // UnityAwake
    void Awake() {
        _movement = GetComponent<GridMovement>();
    }

    // Unity Start
    void Start() {
        SubscribeManager();
    }

    public override bool CanMove() {

        if (_movement.Builder().MosueOverGrid()) {
            if (_movement.Builder().GetMouseGridPlane().node.walkable) {
                _movement.CalcRoute(transform.position, _movement.Builder().GetMouseGridPlane());
                _movement.Builder().DisplayPath(_movement.Route);
            }
        }

        return (uCore.Action.GetKeyDown(KeyCode.Space));
    }
    public override void Move() {
        base.Move();

        _movement.SetDestination(transform.position, _movement.Builder().GetMouseGridPlane());
        _movement.onDestinationReached += EndMovement;

    }

    public override bool CanAct() {
        return (uCore.Action.GetKeyDown(UnityEngine.KeyCode.Space));
    }
    public override void Act() {
        base.Act();

    }


}
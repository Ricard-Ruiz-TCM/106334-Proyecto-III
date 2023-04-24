using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Enemy : Actor {

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

    public override bool CanAct() { return acting.Equals(progress.ready) && !moving.Equals(progress.doing); }
    public override void Act() {
        base.Act();
        Debug.Log("ENEMY ACT");
        EndAction();
    }

    public override bool CanMove() { return moving.Equals(progress.ready) && !acting.Equals(progress.doing); }
    public override void Move() {
        base.Move();

        GridPlane plane;
        do {
            plane = _movement.Builder().GetGridPlane(Random.Range(0, 7), Random.Range(0, 7));
        } while (!plane.node.walkable);

        _movement.SetDestination(transform.position, plane,_statistics.Movement);
        _movement.onDestinationReached += EndMovement;

    }

}
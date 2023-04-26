using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Enemy : Actor {

    [SerializeField] CameraController cameraController;

    // Unity Awake
    protected override void Awake() {
        base.Awake();
    }

    // Unity Start
    void Start() {
        SubscribeManager();
        BuildSkills();
    }

    public override bool CanAct() { return acting.Equals(progress.ready) && !moving.Equals(progress.doing); }
    public override void Act() {
        base.Act();

        Debug.Log("ENEMY ACT");
        EndAction();
    }

    public override bool CanMove() { cameraController.ChangeTarget(transform); return moving.Equals(progress.ready) && !acting.Equals(progress.doing); }
    public override void Move() {
        base.Move();

        GridPlane plane;
        do {
            plane = _gridMovement.Builder().GetGridPlane(Random.Range(0, 7), Random.Range(0, 7));
        } while (!plane.node.walkable);

        _gridMovement.SetDestination(transform.position, plane, _movement);
        _gridMovement.onDestinationReached += EndMovement;

    }

}
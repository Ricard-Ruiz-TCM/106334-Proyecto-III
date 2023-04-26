using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Enemy : Actor 
{


    [SerializeField]
    private enemyCombatStyle _combatStyle;

    // Unity Awake
    protected override void Awake() {
        base.Awake();
    }

    // Unity Start
    void Start() {
        SubscribeManager();
        BuildSkills();

        if (_weapon._item.Equals(items.Bow)) {
            _combatStyle = enemyCombatStyle.ranged;
        } else {
            _combatStyle = enemyCombatStyle.melee;
        }
    }

    public override bool CanAct() { return acting.Equals(progress.ready) && !moving.Equals(progress.doing); }
    public override void Act() {
        base.Act();


        
        EndAction();
    }

    public override bool CanMove() { return uCore.Action.GetKeyDown(KeyCode.P) && moving.Equals(progress.ready) && !acting.Equals(progress.doing) && canMove; }
    public override void Move() {
        base.Move();
        /*
        GridPlane plane;
        do {
            plane = _gridMovement.Builder().GetGridPlane(Random.Range(0, 7), Random.Range(0, 7));
        } while (!plane.node.walkable);

        _gridMovement.SetDestination(transform.position, plane, _movement);
        _gridMovement.onDestinationReached += EndMovement;
        */
    }

}
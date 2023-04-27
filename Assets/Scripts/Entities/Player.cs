using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Player : Actor {

    bool moveController = true;
    // Unity Awake
    protected override void Awake() {
        base.Awake();
    }

    // Unity Start
    protected virtual void Start() {
        SubscribeManager();

        FindObjectOfType<InventoryUI>().AsignInventory(_inventory);
        FindObjectOfType<InventoryUI>().UpdateInventory(_inventory);

        BuildSkills();

    }

    public override bool CanMove() {
        if (hasTurnEnded)
            return false;

        if (_gridMovement.Builder().MosueOverGrid() && canMove) {
            moveController = true;
            if (_gridMovement.Builder().GetMouseGridPlane().node.walkable) {
                _gridMovement.CalcRoute(transform.position, _gridMovement.Builder().GetMouseGridPlane(), Movement());
                _gridMovement.Builder().DisplayValidPath(_gridMovement.VisualRouteValid, Movement());
                _gridMovement.Builder().DisplayInValidPath(_gridMovement.VisualRouteInvaild);

            }
        }
        //else if (!canMove && moveController)
        //{
        //    moveController = false;
        //    _gridMovement.Builder().ClearGrid();
        //}
        return (uCore.Action.GetKeyDown(KeyCode.M) && canMove);

    }
    public override void Move() {
        base.Move();
        GridPlane gr = _gridMovement.Builder().GetMouseGridPlane();
        if ((gr != null) && (gr.node.walkable)) {
            _gridMovement.SetDestination(transform.position, gr, Movement());
            _gridMovement.onDestinationReached += EndMovement;
        }

    }

    public override bool CanAct() {
        return (uCore.Action.GetKeyDown(KeyCode.A));
    }
    public override void Act() {
        base.Act();

        _inventory.AddItem(items.Bow);

        Damage();

        EndAction();

        _gridMovement.Builder().ClearGrid();

    }


}

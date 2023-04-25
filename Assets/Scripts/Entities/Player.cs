using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Player : Actor {

    // Grid Movement
    protected GridMovement _gridMovement;

    [SerializeField] CameraController cameraController;

    // UnityAwake
    protected void Awake() {
        _gridMovement = GetComponent<GridMovement>();
    }

    // Unity Start
    protected virtual void Start() {
        SubscribeManager();

        FindObjectOfType<InventoryUI>().AsignInventory(_inventory);
        FindObjectOfType<InventoryUI>().UpdateInventory(_inventory);

        //_statistics.AddSkill();

    }

    public override bool CanMove() {
        cameraController.ChangeTarget(transform);
        if (_gridMovement.Builder().MosueOverGrid()) {
            if (_gridMovement.Builder().GetMouseGridPlane().node.walkable) {
                _gridMovement.CalcRoute(transform.position, _gridMovement.Builder().GetMouseGridPlane());
                _gridMovement.Builder().DisplayPath(_gridMovement.VisualRoute, _movement);
            }
        }

        return (uCore.Action.GetKeyDown(KeyCode.M));
    }
    public override void Move() {
        base.Move();

        _gridMovement.SetDestination(transform.position, _gridMovement.Builder().GetMouseGridPlane(), _movement);
        _gridMovement.onDestinationReached += EndMovement;

    }

    public override bool CanAct() {
        return (uCore.Action.GetKeyDown(KeyCode.A));
    }
    public override void Act() {
        base.Act();

        _inventory.AddItem(items.Bow);

        EndAction();

    }


}

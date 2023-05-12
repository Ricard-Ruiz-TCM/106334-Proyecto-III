using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(GridMovement)), RequireComponent(typeof(ActorPerks))]
[RequireComponent(typeof(ActorSkills)), RequireComponent(typeof(ActorStatus))]
[RequireComponent(typeof(ActorInventory))]
public class Player : Actor {

    bool moveController = true;

    [SerializeField] SkinnedMeshRenderer skinnedMesh;
    [SerializeField] Material shaderMat;
    [SerializeField] VisualEffect effect;

    // Unity Awake
    protected override void Awake() {
        base.Awake();

        CanBePlaced = true;
    }

    // Unity Start
    protected virtual void Start() {
        SubscribeManager();

        BuildSkills();

        transform.position = Stage.StageManager.RandomInnitialPosition();

    }

    private void Update()
    {
        if (uCore.Action.GetKeyDown(KeyCode.Q))
        {
            skinnedMesh.material = shaderMat;
            effect.Play();
        }
    }

    public void Placing() {

        if (Stage.StageBuilder.MosueOverGrid()) {
            if (Stage.StageBuilder.GetMouseGridPlane().node.type.Equals(Array2DEditor.nodeType.P)) {
                if (uCore.Action.GetKeyDown(KeyCode.M)) {
                    transform.position = Stage.StageBuilder.GetMouseGridPlane().position;
                }
            }
        }

    }

    public override bool CanMove() {
        if (hasTurnEnded)
            return false;

        if (Stage.StageBuilder.MosueOverGrid() && canMove) {
            moveController = true;
            if (Stage.StageBuilder.GetMouseGridPlane().node.walkable) {
                _gridMovement.CalcRoute(transform.position, Stage.StageBuilder.GetMouseGridPlane(), Movement());
                Stage.StageBuilder.DisplayValidPath(_gridMovement.VisualRouteValid, Movement());
                Stage.StageBuilder.DisplayInValidPath(_gridMovement.VisualRouteInvaild);
            }
        }
        //else if (!canMove && moveController)
        //{
        //    moveController = false;
        //    Stage.StageBuilder.ClearGrid();
        //}
        return (uCore.Action.GetKeyDown(KeyCode.M) && canMove);

    }
    public override void Move() {
        base.Move();
        GridPlane gr = Stage.StageBuilder.GetMouseGridPlane();
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

        //_inventory.AddItem(items.Bow);

        Damage();

        EndAction();

        Stage.StageBuilder.ClearGrid();

    }


}

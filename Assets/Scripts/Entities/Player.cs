using System;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(GridMovement)), RequireComponent(typeof(ActorPerks))]
[RequireComponent(typeof(ActorSkills)), RequireComponent(typeof(ActorStatus))]
[RequireComponent(typeof(ActorInventory))]
public class Player : Actor {

    public static event Action onPlayerDie;
    public static event Action onPlayerstep;

    bool moveController = true;

    [SerializeField] SkinnedMeshRenderer skinnedMesh;
    [SerializeField] Material shaderMat;
    [SerializeField] VisualEffect effect;

    /** Animator Controller */
    private Animator _animator;

    // Unity Awake
    protected override void Awake() {
        base.Awake();

        _animator = GetComponent<Animator>();

        CanBePlaced = true;
    }

    // Unity Start
    protected virtual void Start() {
        SubscribeManager();

        BuildSkills();

        transform.position = Stage.StageManager.RandomInnitialPosition();

        _gridMovement.onStepReached += (Array2DEditor.nodeType t) => { onPlayerstep?.Invoke(); };
    }

    private void Update()
    {
        if (uCore.Action.GetKeyDown(KeyCode.Q))
        {
            skinnedMesh.material = shaderMat;
            effect.Play();
        }

        if (uCore.Action.GetKeyDown(KeyCode.K)) {
            TakeDamage(100);
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
        _animator.SetBool("moving", false);
    }

    GridPlane plane = null;

    public override bool CanMove() {
        if (hasTurnEnded)
            return false;

        GridPlane currentPlane = Stage.StageBuilder.GetMouseGridPlane();

        if ((plane != currentPlane) && canMove && (currentPlane != null)) {
            plane = currentPlane;
            moveController = true;
            if (plane.node.walkable) {
                GridM().CalcRoute(transform.position, plane, Movement());
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
            _animator.SetBool("moving", true);
        }

    }

    public override void EndMovement() {
        base.EndMovement();
        _animator.SetBool("moving", false);
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

    public override void Die() {
        onPlayerDie?.Invoke();
    }
}

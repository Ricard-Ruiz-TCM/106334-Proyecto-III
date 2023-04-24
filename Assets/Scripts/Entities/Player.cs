using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GridMovement))]
public class Player : Actor
{

    // Grid Movement
    protected GridMovement _movement;

    // UnityAwake
    protected void Awake()
    {
        _movement = GetComponent<GridMovement>();
    }

    // Unity Start
    protected void Start()
    {
        SubscribeManager();
    }

    public override bool CanMove()
    {
        Debug.Log("aa");
        if (_movement.Builder().MosueOverGrid())
        {
            if (_movement.Builder().GetMouseGridPlane().node.walkable)
            {
                _movement.CalcRoute(transform.position, _movement.Builder().GetMouseGridPlane());
                _movement.Builder().DisplayPath(_movement.VisualRoute);
            }
        }

        return (uCore.Action.GetKeyDown(KeyCode.M));
    }
    public override void Move()
    {
        base.Move();

        _movement.SetDestination(transform.position, _movement.Builder().GetMouseGridPlane(), _statistics.Movement);
        _movement.onDestinationReached += EndMovement;

    }

    public override bool CanAct()
    {
        return (uCore.Action.GetKeyDown(KeyCode.A));
    }
    public override void Act()
    {
        base.Act();

        Debug.Log("PLAYER ACT");

        EndAction();

    }


}

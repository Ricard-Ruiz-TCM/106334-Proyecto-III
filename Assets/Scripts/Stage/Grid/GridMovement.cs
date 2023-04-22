using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridMovement : MonoBehaviour {

    public Action onDestinationReached;

    // NavMesh Agent
    private NavMeshAgent _agent;

    // Pathfinder y Builder del escenario
    private Pathfinding _pathfinder;
    private GridBuilder _builder;
    public GridBuilder Builder() {
        return _builder;
    }

    [SerializeField]
    private bool _canMove;

    // Destination path
    private int _index;
    public List<Node> VisualRoute;
    private List<Node> _destionationRoute;

    // Unity Awake
    void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _builder = GetComponent<GridBuilder>();
    }

    // Start is called before the first frame update
    void Start() {
        _pathfinder = GameObject.FindObjectOfType<Pathfinding>();
        _builder = GameObject.FindObjectOfType<GridBuilder>();
    }

    /** Establece el origen y el destino del movimiento */
    public void SetDestination(Vector3 origin, GridPlane plane) {
        _canMove = true; _index = -1;
        _destionationRoute = _pathfinder.FindPath(_builder.GetGridPlane(origin).node, plane.node);
        NextPoint();
    }

    public void CalcRoute(Vector3 origin, GridPlane plane) {
        VisualRoute = _pathfinder.FindPath(_builder.GetGridPlane(origin).node, plane.node);
    }

    // Unity Update
    void Update() {
        if (!_canMove)
            return;

        if (DestinationReached())
            NextPoint();
    }

    /** Comprueba si hemos llegado al punto */
    public bool DestinationReached() {
        return !_agent.hasPath && !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    /** M�todo para ir al siguiente nodo */
    private void NextPoint() {
        if (_index < _destionationRoute.Count - 1) {
            _index++;
            _agent.SetDestination(_builder.GetGridPlane(_destionationRoute[_index]).position);
        } else {
            _canMove = false;
            onDestinationReached?.Invoke();
        }
    }

}

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

    // Destination path
    private int _idnex = -1;
    public List<Node> Route;

    [SerializeField, Header("Threshold:")]
    private float pathEndThreshold;

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
        _idnex = 0;
        CalcRoute(origin, plane);
    }

    public void CalcRoute(Vector3 origin, GridPlane plane) {
        Route = _pathfinder.FindPath(_builder.GetGridPlane(origin).node, plane.node);
    }

    // Unity Update
    void Update() {
        if (Route == null || _idnex == -1)
            return;

        if (!_agent.hasPath || _agent.remainingDistance <= _agent.stoppingDistance + pathEndThreshold) {
            NextPathNode();
        }
    }

    /** Método para ir al siguiente nodo */
    private void NextPathNode() {
        _idnex++;
        if (_idnex == Route.Count) {
            Route = null;
            onDestinationReached?.Invoke();
        } else {
            _agent.SetDestination(_builder.GetGridPlane(Route[_idnex]).position);
        }
    }

}

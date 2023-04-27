using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridMovement : MonoBehaviour
{

    // callback
    public Action onStepReached;
    public Action onDestinationReached;

    // NavMesh Agent
    private NavMeshAgent _agent;

    // Pathfinder y Builder del escenario
    private Pathfinding _pathfinder;
    private GridBuilder _builder;
    public GridBuilder Builder()
    {
        return _builder;
    }

    public bool _canMove;

    // Destination path
    private int _index;
    public List<Node> VisualRouteValid;
    public List<Node> VisualRouteInvaild;
    private List<Node> _destionationRoute;

    // Unity Awake
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _builder = GetComponent<GridBuilder>();
        VisualRouteValid = new List<Node>();
        VisualRouteInvaild = new List<Node>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _pathfinder = GameObject.FindObjectOfType<Pathfinding>();
        _builder = GameObject.FindObjectOfType<GridBuilder>();
    }

    /** Establece el origen y el destino del movimiento */
    public void SetDestination(Vector3 origin, GridPlane plane, int amount)
    {
        _canMove = true;
        _index = -1;
        _destionationRoute = new List<Node>();
        List<Node> tmp = _pathfinder.FindPath(_builder.GetGridPlane(origin).node, plane.node);
        for (int i = 0; i < Mathf.Min(tmp.Count, amount); i++)
        {
            _destionationRoute.Add(tmp[i]);
        }
        NextPoint();
    }

    public List<Node> CalcRoute(Vector3 origin, GridPlane plane, int amount = -1)
    {
        List<Node> route = _pathfinder.FindPath(_builder.GetGridPlane(origin).node, plane.node);
        VisualRouteValid.Clear();
        VisualRouteInvaild.Clear();
        if (amount != -1)
        {
            for (int i = 0; i < MathF.Min(amount, route.Count); i++)
            {
                VisualRouteValid.Add(route[i]);
            }
            for (int i = VisualRouteValid.Count; i < route.Count; i++)
            {
                VisualRouteInvaild.Add(route[i]);
            }
        }
        else
        {
            VisualRouteValid = route;
        }
        return route;
    }

    public int StepsRemain()
    {
        if (_destionationRoute == null)
            return 0;

        return _destionationRoute.Count - _index;
    }

    // Unity Update
    void Update()
    {
        if (!_canMove)
            return;

        if (DestinationReached())
            NextPoint();
    }

    /** Comprueba si hemos llegado al punto */
    public bool DestinationReached()
    {
        return !_agent.hasPath && !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    /** Método para ir al siguiente nodo */
    private void NextPoint()
    {
        if (_index < _destionationRoute.Count - 1)
        {
            _index++;
            _agent.SetDestination(_builder.GetGridPlane(_destionationRoute[_index]).position);
            onStepReached?.Invoke();
        }
        else
        {
            _canMove = false;
            onDestinationReached?.Invoke();
        }
    }

    public Vector2 GetLastNode()
    {
        return new Vector2(_destionationRoute[_destionationRoute.Count - 1].x, _destionationRoute[_destionationRoute.Count - 1].y);
    }

}

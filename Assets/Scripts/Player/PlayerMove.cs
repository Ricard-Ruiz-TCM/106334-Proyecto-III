using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : AnimatedState {

    private NavMeshAgent _agent;

    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
    }

    public override void CreateTransitions() {
        StateTransition toIddle = new StateTransition
            (() => { return _agent.remainingDistance <= 0.1f; });
        AddTransition(toIddle, PlayerStates.Iddle);
    }

    public override void OnEnter() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.Log("HOl2);");

        if (Physics.Raycast(ray, out hit)) {
            _agent.SetDestination(hit.point);
        }

    }

    public override void OnState() {
        
    }

    public override void OnExit() {
        
    }

}

using UnityEngine;

public class PlayerMove : AnimatedState {

    public override void CreateTransitions() {
        StateTransition toIddle = new StateTransition(
            () => { return false; },
            () => { Debug.Log("TO IDDLE FROM MOVE TRIGGER"); }
        );
        AddTransition(toIddle, PlayerStates.Iddle);
    }

    public override void OnEnter() {
        Debug.Log("Move OnEnter");
    }

    public override void OnState() {
        Debug.Log("Move OnUpdate");
    }

    public override void OnExit() {
        Debug.Log("Move OnExit");
    }

}
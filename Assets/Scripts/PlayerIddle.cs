using UnityEngine;

public class PlayerIddle : AnimatedState {

    public override void CreateTransitions() {
        StateTransition toMove = new StateTransition(
            () => { return false; },
            () => { Debug.Log("Move Trigger"); }
        ); AddTransition(toMove, PlayerStates.Move);

        StateTransition toJump = new StateTransition(
            () => { return false; },
            () => { Debug.Log("Jump Trigger"); }
        ); AddTransition(toJump, PlayerStates.Jump);
    }

    public override void OnEnter() { Debug.Log("Iddle OnEnter"); }
    public override void OnState() { Debug.Log("Iddle OnUpdate"); }
    public override void OnExit() { Debug.Log("Iddle OnExit"); }

}
using UnityEngine;

public class PlayerIddle : AnimatedState {

    public override void CreateTransitions() {
        StateTransition toMove = new StateTransition(
            () => { return uCore.Action.MoveForward(); },
            () => { Debug.Log("Move Trigger"); }
        ); AddTransition(toMove, PlayerController.Move);

        StateTransition toJump = new StateTransition(
            () => { return uCore.Action.Jump(); },
            () => { Debug.Log("Jump Trigger"); }
        ); AddTransition(toJump, PlayerController.Jump);
    }

    public override void OnEnter() { Debug.Log("Iddle OnEnter"); }
    public override void OnState() { Debug.Log("Iddle OnUpdate"); }
    public override void OnExit() { Debug.Log("Iddle OnExit"); }

}
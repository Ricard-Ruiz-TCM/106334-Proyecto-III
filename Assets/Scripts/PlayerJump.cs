using UnityEngine;

public class PlayerJump : AnimatedState {

    public override void CreateTransitions() {
        StateTransition toIddle = new StateTransition(
            () => { return !uCore.Action.Jump(); },
            () => { Debug.Log("TO IDDLE FROM JUMP TRIGGER"); }
        ); AddTransition(toIddle, PlayerController.Iddle);
    }

    public override void OnEnter() { Debug.Log("Jump OnEnter"); }
    public override void OnState() { Debug.Log("Jump OnUpdate"); }
    public override void OnExit() { Debug.Log("Jump OnExit"); }

}
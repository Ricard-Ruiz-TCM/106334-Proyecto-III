using UnityEngine;

public class PlayerIddle : AnimatedState {

    public override void CreateTransitions() {
        StateTransition toMove = new StateTransition
            (() => { return uCore.Action.GetKeyDown(KeyCode.Mouse0); });
        AddTransition(toMove, PlayerStates.Move);
    }

    public override void OnEnter() {
        
    }
    public override void OnState() {
        
    }
    public override void OnExit() {
        
    }

}

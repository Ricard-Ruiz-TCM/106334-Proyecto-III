using UnityEngine;

public class EnemyIddle : AnimatedState {

    // Transitions Override
    public override void CreateTransitions() {
        /** Example
        StateTransition transition = new StateTransition(
            () => { return fales }, // Condition
            () => { } // Callback for "in middle" on last state OnExit and new state OnEnter
        ); AddTransition(transition, basicState obj);
        */
    }

    public override void OnEnter() {

    }

    public override void OnState() {

    }

    public override void OnExit() { 

    }

}

public class Enemy : Actor {

    protected override void ConstructMachine() {

        // Loading Component States
        StateMachine.LoadStates();

        // InnitialState
        // StateMachine.InnitialState = ;
    }

    protected new void Awake() {
        base.Awake();
    }

    protected new void Start() {
        base.Start();
    }

    protected new void Update() {
        base.Update();
    }

}
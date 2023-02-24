
public struct PlayerController {
    public static Player Actor;
    public static PlayerIddle Iddle;
    public static PlayerMove Move;
    public static PlayerJump Jump;
}

public class Player : Actor {

    protected override void ConstructMachine() {

        PlayerController.Actor = this;

        PlayerController.Iddle = GetComponent<PlayerIddle>();
        PlayerController.Move = GetComponent<PlayerMove>();
        PlayerController.Jump = GetComponent<PlayerJump>();

        StateMachine.LoadStates(GetComponents<BasicState>());

        StateMachine.InnitialState = PlayerController.Iddle;
    }

    public override void OnAwake() {

    }

    public override void OnStart() {

    }

    public override void OnUpdate() {

    }

    public override void OnFixedUpdate() {

    }

}
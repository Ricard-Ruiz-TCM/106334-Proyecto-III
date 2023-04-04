using UnityEngine;

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

        StateMachine.LoadStates();

        StateMachine.InnitialState = PlayerController.Iddle;

        CreateTransitions(PlayerController.Iddle, PlayerController.Jump,
            () => { return false; });
    }

    protected new void Awake() {
        base.Awake();
    }

    protected new void Start() {
        base.Start();
    }

    protected new void Update() {
        base.Update();

        if (uCore.Action.GetKeyDown(KeyCode.A)) {
            uCore.Audio.PlaySoundtrack("pop").FadeIn(3f).persistent();
        }
        if (uCore.Action.GetKeyDown(KeyCode.C)) {
            uCore.Audio.PlaySFX("coin", transform);
        }

        if (uCore.Action.GetKeyDown(KeyCode.B)) {
            uCore.Audio.PlaySoundtrack("pop").FadeOut(2f);
        }

    }



}
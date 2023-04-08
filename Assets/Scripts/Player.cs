using UnityEngine;
using UnityEngine.Rendering.Universal;

public struct PlayerStates {
    public static PlayerIddle Iddle;
    public static PlayerMove Move;
    public static PlayerJump Jump;
}

public class Player : Actor {

    protected override void ConstructMachine() {

        PlayerStates.Iddle = GetComponent<PlayerIddle>();
        PlayerStates.Move = GetComponent<PlayerMove>();
        PlayerStates.Jump = GetComponent<PlayerJump>();

        StateMachine.LoadStates();

        CreateTransitions(PlayerStates.Iddle, PlayerStates.Jump,
            () => { return false; });

        StateTransition transition = new StateTransition(
            () => { return false; },
            () => { });

        AddTransition(PlayerStates.Move, transition, PlayerStates.Iddle);

        StateMachine.InnitialState = PlayerStates.Iddle;
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
            uCore.Effects.CanvasFadeOut(5f, () => { uCore.Effects.CameraShake(20f, 0.2f); });   
        }

        if (uCore.Action.GetKeyDown(KeyCode.B)) {
            uCore.Audio.PlaySoundtrack("pop").FadeOut(2f);
        }

    }



}
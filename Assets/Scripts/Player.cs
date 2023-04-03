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
            uCore.Audio.PlaySFX("coin");
        }
        if (uCore.Action.GetKeyDown(KeyCode.B)) {
            uCore.Audio.PlaySFX("coin").persistent();
        }
        if (uCore.Action.GetKeyDown(KeyCode.C)) {
            uCore.Audio.PlaySFX("boom", new Vector3(4.0f, 0.0f, 2.0f)).persistent().looped().onMinMaxDistance(3.0f, 52.0f);
        }
        if (uCore.Action.GetKeyDown(KeyCode.D)) {
            uCore.Audio.PlaySFX("boom", GameObject.Find("Cube").transform);
        }
        if (uCore.Action.GetKeyDown(KeyCode.E)) {
            uCore.Audio.PlaySoundTrack("pop");
        }

    }



}
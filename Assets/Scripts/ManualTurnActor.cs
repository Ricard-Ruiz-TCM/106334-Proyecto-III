using UnityEngine;

public class ManualTurnActor : ManualTurnable {

    private void Start() {
        SubscribeManager();
    }

    public override void Act() {
        base.Act();

        Debug.Log("Action MANUAL");

        Invoke("EndAction", 3f);
    }

    public override bool CanAct() {
        return uCore.Action.GetKeyDown(KeyCode.Space);
    }

    public override void Move() {
        base.Move();

        Debug.Log("Move MANUAL");

        Invoke("EndMovement", 3f);
    }

    public override bool CanMove() {
        return uCore.Action.GetKeyDown(KeyCode.A);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTurnActor : ManualTurnable {


    private void Start() {
        SubscribeManager();
    }

    public override void Act() {
        StartAct();
        Debug.Log("Action MANUAL");
        Invoke("EndAction", 3f);
    }

    public override bool CanAct() {
        return uCore.Action.GetKeyDown(KeyCode.Space);
    }

    public override void Move() {
        StartMove();
        Debug.Log("Move MANUAL");
        Invoke("EndMovement", 3f);
    }

    public override bool CanMove() {
        return uCore.Action.GetKeyDown(KeyCode.A);
    }

}

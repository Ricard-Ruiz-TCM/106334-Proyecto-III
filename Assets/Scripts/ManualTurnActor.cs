using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTurnActor : ManualTurnable {


    private void Start() {
        AddToManager();
    }

    public override void Act() {
        Debug.Log("Action MANUAL");
        hasActed = true;
    }

    public override bool CanAct() {
        return uCore.Action.GetKeyDown(KeyCode.Space);
    }

    public override void Move() {
        Debug.Log("Move MANUAL");
        hasMoved = true;
    }

    public override bool CanMove() {
        return uCore.Action.GetKeyDown(KeyCode.A);
    }

}

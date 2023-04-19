using System;
using System.Collections;
using UnityEngine;

public class AutoTurnActor_B : AutomaticTurnable {

    // Unity Start
    void Start() {
        SubscribeManager();
    }

    public override void Act() {
        StartAct();

        Debug.Log("ACTING B");

        Invoke("EndAction", 3f);
    }

    public override void Move() {
        StartMove();

        Debug.Log("MOVING B");

        Invoke("EndMovement", 3f);
    }

}

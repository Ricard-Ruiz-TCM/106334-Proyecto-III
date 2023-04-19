using System;
using System.Collections;
using UnityEngine;


public class AutoTurnActor_A : AutomaticTurnable {

    // Unity Start
    void Start() {
        SubscribeManager();
    }

    public override void Act() {
        StartAct();

        Debug.Log("ACTING A");

        Invoke("EndAction", 3f);
    }

    public override void Move() {
        StartMove();

        Debug.Log("MOVING A");

        Invoke("EndMovement", 3f);
    }
}

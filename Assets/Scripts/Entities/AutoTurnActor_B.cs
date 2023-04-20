using UnityEngine;

public class AutoTurnActor_B : AutomaticTurnable {

    // Unity Start
    void Start() {
        SubscribeManager();
    }

    public override void Act() {
        base.Act();

        Debug.Log("ACTING B");

        Invoke("EndAction", 3f);
    }

    public override void Move() {
        base.Move();

        Debug.Log("MOVING B");

        Invoke("EndMovement", 3f);
    }

}

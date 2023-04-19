using UnityEngine;


public class AutoTurnActor_A : AutomaticTurnable {

    // Unity Start
    void Start() {
        SubscribeManager();
    }

    public override void Act() {
        base.Act();

        Debug.Log("ACTING A");

        Invoke("EndAction", 3f);
    }

    public override void Move() {
        base.Move();

        Debug.Log("MOVING A");

        Invoke("EndMovement", 3f);
    }
}

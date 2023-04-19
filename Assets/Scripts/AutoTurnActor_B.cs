using System.Collections;
using UnityEngine;

public class AutoTurnActor_B : AutomaticTurnable {

    // Unity Start
    void Start() {
        AddToManager();
    }

    public override IEnumerator C_Act() {
        Debug.Log("ACTING B");
        yield return new WaitForSeconds(3f);
        EndAction();
    }

    public override IEnumerator C_Move() {
        Debug.Log("MOVING B");
        yield return new WaitForSeconds(3f);
        EndMovement();
    }


}

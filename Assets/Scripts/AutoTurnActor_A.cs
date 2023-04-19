using System.Collections;
using UnityEngine;


public class AutoTurnActor_A : AutomaticTurnable {

    // Unity Start
    void Start() {
        AddToManager();
    }

    public override IEnumerator C_Act() {
        Debug.Log("ACTING A");
        yield return new WaitForSeconds(3f);
        EndAction();
    }

    public override IEnumerator C_Move() {
        Debug.Log("MOVING A");
        yield return new WaitForSeconds(3f);
        EndMovement();
    }


}

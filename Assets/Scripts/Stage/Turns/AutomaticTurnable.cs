using System.Collections;
using UnityEngine;

public abstract class AutomaticTurnable : ManualTurnable {

    public override bool CanAct() {
        return acting.Equals(progress.ready) && !moving.Equals(progress.doing);
    }

    public override bool CanMove() {
        return moving.Equals(progress.ready) && !acting.Equals(progress.doing);
    }

}

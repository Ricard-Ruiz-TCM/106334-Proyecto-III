﻿using UnityEngine;

[CreateAssetMenu(fileName = "Stunned", menuName = "Combat/Buffs/Stunned")]
public class Stunned : Buff {

    public override void onApply(Turnable me) {
        Debug.Log("TODO: Apply Stunned Feedback");
    }

    public override void startTurnEffect(Turnable me) {
        me.endAction();
        me.endMovement();
    }

    public override void endTurnEffect(Turnable me) {
    }

}

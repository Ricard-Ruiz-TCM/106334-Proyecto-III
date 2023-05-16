using UnityEngine;

public abstract class ModBuff : Buff {

    [Header("Effect on:")]
    public modType type;
    [Header("Value:")]
    public int value;
    [Header("How affect:")]
    public modOperation operation;

    public override void startTurnEffect(Turnable me) {
    }

    public override void endTurnEffect(Turnable me) {
    }

}

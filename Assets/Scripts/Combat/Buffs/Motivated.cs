using UnityEngine;

[CreateAssetMenu(fileName = "Motivated", menuName = "Combat/Buffs/Motivated")]
public class Motivated : Buff {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Invisible Feedback + extras.");
    }

    public override void onRemove(BasicActor me) {
        Debug.Log("TODO: Remove Invisible Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Invisible Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Invisible Feedback");
    }

}

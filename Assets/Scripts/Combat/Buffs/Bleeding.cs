using UnityEngine;

[CreateAssetMenu(fileName = "Bleeding", menuName = "Combat/Buffs/Bleeding")]
public class Bleeding : DoT {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Bleeding Feedback + extras.");
    }

    public override void onRemove(BasicActor me) {
        Debug.Log("TODO: Remove Bleeding Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Bleeding Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        base.endTurnEffect(me);
        Debug.Log("TODO: End Turn Bleeding Feedback");
    }

}

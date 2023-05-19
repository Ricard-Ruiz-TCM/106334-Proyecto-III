using UnityEngine;

[CreateAssetMenu(fileName = "MidDefense", menuName = "Combat/Buffs/Mid Defense")]
public class MidDefense : ModBuff {

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

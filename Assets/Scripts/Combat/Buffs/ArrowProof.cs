using UnityEngine;

[CreateAssetMenu(fileName = "ArrowProof", menuName = "Combat/Buffs/ArrowProof")]
public class ArrowProof : Buff {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply ArrowProof Feedback");
    }

    public override void onRemove(BasicActor me) {
        Debug.Log("TODO: Remove ArrowProof Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn ArrowProof Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn ArrowProof Feedback");
    }

}

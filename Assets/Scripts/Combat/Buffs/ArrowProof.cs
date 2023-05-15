using UnityEngine;

[CreateAssetMenu(fileName = "ArrowProof", menuName = "Combat/Buffs/ArrowProof")]
public class ArrowProof : Buff {

    public override void onApply(Actor me) {
        Debug.Log("TODO: Apply Stunned Feedback");
    }

    public override void startTurnEffect(Actor me) {
    }

    public override void endTurnEffect(Actor me) {
    }

}

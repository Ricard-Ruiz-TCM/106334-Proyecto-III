using UnityEngine;

[CreateAssetMenu(fileName = "ArrowProof", menuName = "Combat/Buffs/ArrowProof")]
public class ArrowProof : Buff {

    public override void onApply(Turnable me) {
        Debug.Log("TODO: Apply Stunned Feedback");
    }

    public override void startTurnEffect(Turnable me) {
    }

    public override void endTurnEffect(Turnable me) {
    }

}

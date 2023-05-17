using UnityEngine;

[CreateAssetMenu(fileName = "ArrowProof", menuName = "Combat/Buffs/ArrowProof")]
public class ArrowProof : Buff {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Stunned Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
    }

    public override void endTurnEffect(BasicActor me) {
    }

}

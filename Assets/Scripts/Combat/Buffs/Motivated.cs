using UnityEngine;

[CreateAssetMenu(fileName = "Motivated", menuName = "Combat/Buffs/Motivated")]
public class Motivated : Buff {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Motivated Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
    }

    public override void endTurnEffect(BasicActor me) {
    }

}

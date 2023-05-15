using UnityEngine;

[CreateAssetMenu(fileName = "Motivated", menuName = "Combat/Buffs/Motivated")]
public class Motivated : Buff {

    public override void onApply(Actor me) {
        Debug.Log("TODO: Apply Motivated Feedback");
    }

    public override void startTurnEffect(Actor me) {
    }

    public override void endTurnEffect(Actor me) {
    }

}

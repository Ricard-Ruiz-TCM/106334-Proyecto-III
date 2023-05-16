using UnityEngine;

[CreateAssetMenu(fileName = "Motivated", menuName = "Combat/Buffs/Motivated")]
public class Motivated : Buff {

    public override void onApply(Turnable me) {
        Debug.Log("TODO: Apply Motivated Feedback");
    }

    public override void startTurnEffect(Turnable me) {
    }

    public override void endTurnEffect(Turnable me) {
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "Disarmed", menuName = "Combat/Buffs/Disarmed")]
public class Disarmed : Buff {

    public override void onApply(Actor me) {
        Debug.Log("TODO: Apply Disarmed Feedback + extras.");
    }

    public override void endTurnEffect(Actor me) {
    }

    public override void startTurnEffect(Actor me) {
    }
}

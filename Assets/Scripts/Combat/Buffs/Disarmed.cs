using UnityEngine;

[CreateAssetMenu(fileName = "Disarmed", menuName = "Combat/Buffs/Disarmed")]
public class Disarmed : Buff {

    public override void onApply(Turnable me) {
        Debug.Log("TODO: Apply Disarmed Feedback + extras.");
    }

    public override void endTurnEffect(Turnable me) {
    }

    public override void startTurnEffect(Turnable me) {
    }
}

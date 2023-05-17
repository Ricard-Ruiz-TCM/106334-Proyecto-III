using UnityEngine;

[CreateAssetMenu(fileName = "Disarmed", menuName = "Combat/Buffs/Disarmed")]
public class Disarmed : Buff {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Disarmed Feedback + extras.");
    }

    public override void endTurnEffect(BasicActor me) {
    }

    public override void startTurnEffect(BasicActor me) {
    }
}

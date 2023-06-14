using UnityEngine;

[CreateAssetMenu(fileName = "Disarmed", menuName = "Combat/Buffs/Disarmed")]
public class Disarmed : Buff {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Disarmed Feedback + extras.");
    }

    public override void onRemove(BasicActor me) {
        Debug.Log("TODO: Remove Disarmed Feedback");
        me.GetComponent<WeaponHolder>().reArm();
    }

    public override void startTurnEffect(BasicActor me) {
        me.disableAttack();
        Debug.Log("TODO: Start Turn Disarmed Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Disarmed Feedback");
    }

}

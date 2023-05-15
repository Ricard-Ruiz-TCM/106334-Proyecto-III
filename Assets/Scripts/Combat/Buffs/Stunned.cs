using UnityEngine;

[CreateAssetMenu(fileName = "Stunned", menuName = "Combat/Buffs/Stunned")]
public class Stunned : Buff {

    public override void onApply(Actor me) {
        Debug.Log("TODO: Apply Stunned Feedback");
    }

    public override void startTurnEffect(Actor me) {
        me.EndAction();
        me.EndMovement();
    }

    public override void endTurnEffect(Actor me) {
    }

}

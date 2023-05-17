using UnityEngine;

[CreateAssetMenu(fileName = "Stunned", menuName = "Combat/Buffs/Stunned")]
public class Stunned : Buff {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Stunned Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
        me.endAction();
        me.endMovement();
    }

    public override void endTurnEffect(BasicActor me) {
    }

}

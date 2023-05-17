using UnityEngine;

public abstract class DoT : Buff {

    [Header("Damage Amount:")]
    public int damage;

    public override void startTurnEffect(BasicActor me) {
    }

    public override void endTurnEffect(BasicActor me) {
        //me.TakeDamage(damage);
    }

}

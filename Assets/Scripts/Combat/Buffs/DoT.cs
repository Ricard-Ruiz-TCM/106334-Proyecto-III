using UnityEngine;

public abstract class DoT : Buff {

    [Header("Damage Amount:")]
    public int damage;

    public override void startTurnEffect(Actor me) {
    }

    public override void endTurnEffect(Actor me) {
        me.TakeDamage(damage);
    }

}

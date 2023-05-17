using UnityEngine;

public abstract class DoT : Buff {

    [Header("Damage Amount:")]
    public int damage;

    public override void startTurnEffect(Turnable me) {
    }

    public override void endTurnEffect(Turnable me) {
        //me.TakeDamage(damage);
    }

}

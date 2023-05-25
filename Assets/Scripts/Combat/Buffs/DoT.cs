using UnityEngine;

public abstract class DoT : Buff {

    [Header("Damage Amount:")]
    public int damage;

    public override void endTurnEffect(BasicActor me) {
        me.takeDamage((Actor)me, damage);
    }

}

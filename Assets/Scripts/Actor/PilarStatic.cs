


public class PilarStatic : StaticActor {

    Actor damageFrom;

    public override void onActorDeath() {

        base.onActorDeath();
    }

    public override void takeDamage(Actor from, int damage, itemID weapon = itemID.NONE) {
        damageFrom = from;
        base.takeDamage(from, damage, weapon);
    }

}

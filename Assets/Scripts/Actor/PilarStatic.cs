


public class PilarStatic : StaticActor {

    BasicActor damageFrom;

    public override void onActorDeath() {

        base.onActorDeath();
    }

    public override void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {
        damageFrom = from;
        base.takeDamage(from, damage, weapon);
    }

}

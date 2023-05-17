using UnityEngine;

public class StaticActor : BasicActor {

    [SerializeField, Header("Resistencia (a.k.a -> armadura plana):")]
    private int _resistance;

    public override void act() { }

    public override void move() { }

    public override void onActorDeath() {
        GameObject.Destroy(gameObject);
    }

    public override void onTurn() {
        endTurn();
    }

    public override int totalDamage() {
        return 0;
    }

    public override int totalDefense() {
        return _resistance;
    }

}

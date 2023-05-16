using UnityEngine;

public class StaticActor : BasicActor {

    [SerializeField, Header("Resistencia (a.k.a -> armadura plana):")]
    private int _resistance;

    public override void Act() {
        EndAction();
    }

    public override void Move() {
        EndMovement();
    }

    public override void onActorDeath() {
        GameObject.Destroy(gameObject);
    }

    public override int TotalDamage() {
        return 0;
    }

    public override int TotalDefense() {
        return _resistance;
    }

}

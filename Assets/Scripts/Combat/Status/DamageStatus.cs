using UnityEngine;

[CreateAssetMenu(fileName = "new DamageStatus", menuName = "Combat/Status/Damage Status")]
public class DamageStatus : Status {

    [SerializeField, Header("Damage:")]
    private int damage;

    public override void Effect(Actor me) {
        me.TakeDamage(damage);
    }
}

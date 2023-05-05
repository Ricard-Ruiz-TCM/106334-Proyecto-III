using UnityEngine;

[CreateAssetMenu(fileName = "new DamageStatus", menuName = "Combat/Status/Damage Status")]
public class DamageStatus : Status {

    [SerializeField, Header("Damage:")]
    private float damage;

    public override void Effect(Actor me) {
        Debug.Log("DAMAGE STATUS EFFECT FROM " + status.ToString());
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Invencible", menuName = "Combat/Buffs/Invencible")]
public class Invencible : Buff {

    [Header("Material:")]
    public Material material;

    public override void endTurnEffect(Turnable me) {
        me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
    }

    public override void onApply(Turnable me) {
    }

    public override void startTurnEffect(Turnable me) {
    }

}

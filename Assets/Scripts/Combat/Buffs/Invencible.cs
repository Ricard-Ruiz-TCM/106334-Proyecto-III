using UnityEngine;

[CreateAssetMenu(fileName = "Invencible", menuName = "Combat/Buffs/Invencible")]
public class Invencible : Buff {

    [Header("Material:")]
    public Material material;

    public override void endTurnEffect(Actor me) {
        me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
    }

    public override void onApply(Actor me) {
    }

    public override void startTurnEffect(Actor me) {
    }

}

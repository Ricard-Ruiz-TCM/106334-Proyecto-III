using UnityEngine;

[CreateAssetMenu(fileName = "Invisible", menuName = "Combat/Buffs/Invisible")]
public class Invisible : Buff {

    [Header("Invisibility Material:")]
    public Material material;

    public override void onApply(Actor me) {
        me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
    }

    public override void startTurnEffect(Actor me) {
    }

    public override void endTurnEffect(Actor me) {
    }

}

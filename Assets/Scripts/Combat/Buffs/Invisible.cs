using UnityEngine;

[CreateAssetMenu(fileName = "Invisible", menuName = "Combat/Buffs/Invisible")]
public class Invisible : Buff {

    [Header("Invisibility Material:")]
    public Material material;

    public override void onApply(BasicActor me) {
        me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
    }

    public override void startTurnEffect(BasicActor me) {
    }

    public override void endTurnEffect(BasicActor me) {
    }

}

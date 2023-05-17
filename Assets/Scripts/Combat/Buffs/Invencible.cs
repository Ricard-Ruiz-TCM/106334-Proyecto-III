using UnityEngine;

[CreateAssetMenu(fileName = "Invencible", menuName = "Combat/Buffs/Invencible")]
public class Invencible : Buff {

    [Header("Material:")]
    public Material material;

    public override void endTurnEffect(BasicActor me) {
        me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
    }

    public override void onApply(BasicActor me) {
    }

    public override void startTurnEffect(BasicActor me) {
    }

}

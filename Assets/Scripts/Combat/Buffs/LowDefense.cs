using UnityEngine;

[CreateAssetMenu(fileName = "LowDefense", menuName = "Combat/Buffs/Low Defense")]
public class LowDefense : ModBuff {

    public override void onApply(Actor me) {
        Debug.Log("TODO: Apply LowDefense Feedback + extras.");
    }

}

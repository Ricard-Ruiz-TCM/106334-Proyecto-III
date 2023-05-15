using UnityEngine;

[CreateAssetMenu(fileName = "Posioned", menuName = "Combat/Buffs/Posioned")]
public class Posioned : DoT {

    public override void onApply(Actor me) {
        Debug.Log("TODO: Apply Bleeding Feedback + extras.");
    }

}

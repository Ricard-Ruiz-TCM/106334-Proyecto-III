using UnityEngine;

[CreateAssetMenu(fileName = "Bleeding", menuName = "Combat/Buffs/Bleeding")]
public class Bleeding : DoT {

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Bleeding Feedback + extras.");
    }

}

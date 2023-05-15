using UnityEngine;

[CreateAssetMenu(fileName = "Poisoned", menuName = "Combat/Buffs/Poisoned")]
public class Poisoned : DoT {

    public override void onApply(Actor me) {
        Debug.Log("TODO: Apply Poisoned Feedback + extras.");
    }

}

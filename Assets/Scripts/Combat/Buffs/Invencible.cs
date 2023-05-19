using UnityEngine;

[CreateAssetMenu(fileName = "Invencible", menuName = "Combat/Buffs/Invencible")]
public class Invencible : Buff {

    [Header("Material:")]
    public Material material;

    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Invencible Feedback + extras.");
    }

    public override void onRemove(BasicActor me) {
        Debug.Log("TODO: Remove Invencible Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Invencible Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Invencible Feedback");
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "Invisible", menuName = "Combat/Buffs/Invisible")]
public class Invisible : Buff {

    [Header("Invisibility Material:")]
    public Material material;

    public override void onApply(BasicActor me) 
    {
        Debug.Log("TODO: Apply Invisible Feedback + extras.");
        me.GetComponent<MeshTrail>().startInvisible();
    }

    public override void onRemove(BasicActor me) 
    {
        Debug.Log("TODO: Remove Invisible Feedback");
        me.GetComponent<MeshTrail>().endInvisible();
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Invisible Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Invisible Feedback");
    }

}

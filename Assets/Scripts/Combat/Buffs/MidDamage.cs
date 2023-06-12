using UnityEngine;

[CreateAssetMenu(fileName = "MidDamage", menuName = "Combat/Buffs/Mid Damage")]
public class MidDamage : ModBuff {
    [SerializeField] Material mat;
    Material previousMat;

    public override void onApply(BasicActor me) {
        previousMat = me.GetComponent<WeaponHolder>()._gladius.GetComponent<MeshRenderer>().material;
        me.GetComponent<WeaponHolder>()._gladius.GetComponent<MeshRenderer>().material = mat;
        me.GetComponent<WeaponHolder>()._gladius.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("TODO: Apply Invisible Feedback + extras.");
    }

    public override void onRemove(BasicActor me) {
        me.GetComponent<WeaponHolder>()._gladius.GetComponent<MeshRenderer>().material = previousMat;
        me.GetComponent<WeaponHolder>()._gladius.transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("TODO: Remove Invisible Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Invisible Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Invisible Feedback");
    }
}

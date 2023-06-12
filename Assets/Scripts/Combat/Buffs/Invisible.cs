using UnityEngine;


[CreateAssetMenu(fileName = "Invisible", menuName = "Combat/Buffs/Invisible")]
public class Invisible : Buff {

    [Header("Invisibility Ma+terial:")]
    public Material material;
    Material[] invisibleMatList;
    Material weaponMat;
    public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Invisible Feedback + extras.");

        invisibleMatList = new Material[me.skinnedMesh.materials.Length];

        for (int i = 0; i < invisibleMatList.Length; i++) {
            invisibleMatList[i] = material;
        }

        me.skinnedMesh.materials = invisibleMatList;
        me.entitieUI.SetActive(false);

        weaponMat = ((Actor)me).GetComponent<WeaponHolder>().getActiveWeapon().GetComponent<MeshRenderer>().material;

        ((Actor)me).GetComponent<WeaponHolder>().getActiveWeapon().GetComponent<MeshRenderer>().material = material;

    }

    public override void onRemove(BasicActor me) {
        Debug.Log("TODO: Remove Invisible Feedback");
        me.entitieUI.SetActive(true);
        me.skinnedMesh.materials = me.skinnedMaterials;
        ((Actor)me).GetComponent<WeaponHolder>().getActiveWeapon().GetComponent<MeshRenderer>().material = weaponMat;
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Invisible Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Invisible Feedback");
    }

}

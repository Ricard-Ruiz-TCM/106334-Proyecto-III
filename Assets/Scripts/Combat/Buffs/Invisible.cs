﻿using UnityEngine;


[CreateAssetMenu(fileName = "Invisible", menuName = "Combat/Buffs/Invisible")]
public class Invisible : Buff {

    [Header("Invisibility Ma+terial:")]
    public Material material;
    Material[] invisibleMatList;
    Material weaponMat;

    BoxCollider _collider;

public override void onApply(BasicActor me) {
        Debug.Log("TODO: Apply Invisible Feedback + extras.");

        if (_collider == null) {
            _collider = me.gameObject.GetComponent<BoxCollider>();
        }
        _collider.enabled = false;

        invisibleMatList = new Material[me.skinnedMesh.materials.Length];

        for (int i = 0; i < invisibleMatList.Length; i++) {
            invisibleMatList[i] = material;
        }

        me.skinnedMesh.materials = invisibleMatList;
        me.entitieUI.SetActive(false);


    }

    public override void onRemove(BasicActor me) {
        _collider.enabled = true;
        Debug.Log("TODO: Remove Invisible Feedback");
        me.entitieUI.SetActive(true);
        me.skinnedMesh.materials = me.skinnedMaterials;
        me.GetComponent<MeshTrail>().endInvisible();
        me.GetComponent<WeaponHolder>().reArm();
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Invisible Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Invisible Feedback");
    }

}

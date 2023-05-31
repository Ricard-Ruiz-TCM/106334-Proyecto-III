using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ImperialCry", menuName = "Combat/Skills/Imperial Cry")]
public class ImperialCry : Skill {
    
    [SerializeField, Header("Range effect:")]
    private int range = 2;
    public Material mat;
    public GameObject effectPrefab;
    List<Material> anteriorMatarial;

    public override void action(BasicActor from, Node to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.ArrowProof, buffsID.MidDefense);


        from.StartCoroutine(PlayEffect(from));

        // TODO // Instantiate 

        // Apply to me, and others allies 
        foreach (Turnable actor in TurnManager.instance.attenders) {
            if (actor.CompareTag(from.tag) && (Stage.StageBuilder.getDistance(from.transform.position, actor.transform.position) <= range)) {
                BasicActor target = Stage.StageManager.getActor(to);
                if (target != null)
                    ((Actor)target).buffs.applyBuffs((Actor)target, buffsID.ArrowProof, buffsID.MidDefense);
            }
        }
        from.endAction();
    }
    IEnumerator PlayEffect(BasicActor from)
    {
        anteriorMatarial = new List<Material>();
        GameObject effect = Instantiate(effectPrefab, from.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < from.skinnedMaterials.Count; i++)
        {
            anteriorMatarial.Add(from.skinnedMaterials[i]);
            from.skinnedMaterials[i] = mat;
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < from.skinnedMaterials.Count; i++)
        {
            from.skinnedMaterials[i] = anteriorMatarial[i];
        }
        Destroy(effect, 1f);
    }

}

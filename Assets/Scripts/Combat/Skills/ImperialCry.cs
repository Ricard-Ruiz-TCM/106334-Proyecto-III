using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ImperialCry", menuName = "Combat/Skills/Imperial Cry")]
public class ImperialCry : Skill {

    [SerializeField, Header("Range effect:")]
    private int range = 2;
    public Material mat;
    public GameObject effectPrefab;
    //List<Material> anteriorMatarial;

    public override void action(BasicActor from, Node to) {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.UsoHabilidad);
        FMODManager.instance.PlayOneShot(FMODEvents.instance.ImperialCry);
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

    }
    IEnumerator PlayEffect(BasicActor from) {
        GameObject effect = Instantiate(effectPrefab, from.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);


        Material[] prova = new Material[from.skinnedMesh.materials.Length];

        for (int i = 0; i < prova.Length; i++) {
            prova[i] = mat;
        }
        from.skinnedMesh.materials = prova;

        yield return new WaitForSeconds(0.5f);

        from.skinnedMesh.materials = from.skinnedMaterials;

        Destroy(effect, 1f);
        ((Actor)from).Anim.Play("ImperialCry");
        from.endAction();
    }

}

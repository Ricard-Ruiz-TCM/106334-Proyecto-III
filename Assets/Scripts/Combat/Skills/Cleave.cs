using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {

        // bUSCAMOS A LA PEÑA Y APLICAMOS BUFF STUNNED :3 
        List<Node> neight = Stage.Grid.getNeighbours(to);

        neight.Add(to);
        foreach (Node node in neight) {
            BasicActor actor = Stage.StageManager.getActor(node);
            FMODManager.instance.PlayOneShot(FMODEvents.instance.Cleave);
            if ((actor != null) && (actor != from) && (actor is Actor)) {
                FMODManager.instance.PlayOneShot(FMODEvents.instance.Stun);
                Vector3 relativePos = from.transform.position - actor.transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                if (!actor.GetComponent<StaticActor>()) {
                    GameObject blood = Instantiate(bloodPrefab, new Vector3(actor.transform.position.x, actor.transform.position.y + 0.8f, actor.transform.position.z), rotation);
                    Destroy(blood, 2f);
                }

                ((Actor)actor).buffs.applyBuffs((Actor)actor, buffsID.Stunned);
            }
        }

        from.endAction();
    }

}

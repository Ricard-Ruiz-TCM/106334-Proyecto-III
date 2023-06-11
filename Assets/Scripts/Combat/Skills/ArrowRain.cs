using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ArrowRain", menuName = "Combat/Skills/ArrowRain")]
public class ArrowRain : Skill 
{
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {
        uCore.Particles.PlayParticles("ParticulasFlechas", new Vector3(Stage.StageBuilder.getGridPlane(to).position.x, Stage.StageBuilder.getGridPlane(to).position.y + 1.6f, Stage.StageBuilder.getGridPlane(to).position.z));
        List<Node> neight = Stage.Grid.getNeighbours(to);
        FMODManager.instance.PlayOneShot(FMODEvents.instance.LluviaDeFlechas);
        // Appy damage to neightbours 
        foreach (Node node in neight) {
            BasicActor actor = Stage.StageManager.getActor(node);
            if (actor != null) 
            {
                actor.takeDamage((Actor)from, from.totalDamage());
                var lookPos = actor.transform.position - from.transform.position;
                lookPos.y = 0;

                Vector3 relativePos = from.transform.position - actor.transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                GameObject blood = Instantiate(bloodPrefab, new Vector3(actor.transform.position.x, actor.transform.position.y + 0.8f, actor.transform.position.z), rotation);

                Destroy(blood, 2f);
                from.transform.rotation = Quaternion.LookRotation(lookPos);
            }
        }

        // Damage to the target, directly
        base.action(from, to);
    }

}

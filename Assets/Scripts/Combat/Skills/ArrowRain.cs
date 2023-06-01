using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ArrowRain", menuName = "Combat/Skills/ArrowRain")]
public class ArrowRain : Skill 
{
    public override void action(BasicActor from, Node to) {
        uCore.Particles.PlayParticles("ParticulasFlechas", new Vector3(Stage.StageBuilder.getGridPlane(to).position.x, Stage.StageBuilder.getGridPlane(to).position.y + 1.6f, Stage.StageBuilder.getGridPlane(to).position.z));
        List<Node> neight = Stage.Grid.getNeighbours(to);
        // Appy damage to neightbours 
        foreach (Node node in neight) {
            BasicActor actor = Stage.StageManager.getActor(node);
            if (actor != null) 
            {
                actor.takeDamage((Actor)from, from.totalDamage());
                var lookPos = target.transform.position - from.transform.position;
                lookPos.y = 0;
                from.transform.rotation = Quaternion.LookRotation(lookPos);
            }
        }

        // Damage to the target, directly
        base.action(from, to);
    }

}

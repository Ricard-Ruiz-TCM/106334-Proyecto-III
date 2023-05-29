using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ArrowRain", menuName = "Combat/Skills/ArrowRain")]
public class ArrowRain : Skill {

    public override void action(BasicActor from, Node to) {

        List<Node> neight = Stage.Grid.getNeighbours(to);
        // Appy damage to neightbours 
        foreach (Node node in neight) {
            BasicActor actor = Stage.StageManager.getActor(node);
            if (actor != null) {
                actor.takeDamage((Actor)from, from.totalDamage());
            }
        }

        // Damage to the target, directly
        base.action(from, to);
    }

}

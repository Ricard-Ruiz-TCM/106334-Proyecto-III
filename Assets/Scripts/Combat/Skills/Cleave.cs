using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {

    public override void action(BasicActor from, Node to) {

        // bUSCAMOS A LA PEÑA Y APLICAMOS BUFF STUNNED :3 
        List<Node> neight = Stage.Grid.getNeighbours(to);
        neight.Add(to);
        foreach (Node node in neight) {
            BasicActor actor = Stage.StageManager.getActor(node);
            if ((actor != null) && (actor != from) && (actor is Actor)) 
            {
                Debug.Log("sexooooooooooooo");
                ((Actor)actor).buffs.applyBuffs((Actor)actor, buffsID.Stunned);
            }
        }

        from.endAction();
    }

}

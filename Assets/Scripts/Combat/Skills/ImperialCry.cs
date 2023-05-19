using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ImperialCry", menuName = "Combat/Skills/Imperial Cry")]
public class ImperialCry : Skill {
    
    public override void action(BasicActor from, BasicActor to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.ArrowProof, buffsID.MidDefense);

        // TODO // Instantiate 

        // Apply to me, and others allies 
        foreach (Turnable actor in TurnManager.instance.attenders) {
            if (actor.CompareTag(from.tag)) {
                ((Actor)to).buffs.applyBuffs((Actor)to, buffsID.ArrowProof, buffsID.MidDefense);
            }
        }
        from.endAction();
    }

}

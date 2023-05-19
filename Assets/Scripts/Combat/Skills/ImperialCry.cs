using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ImperialCry", menuName = "Combat/Skills/Imperial Cry")]
public class ImperialCry : Skill {
    
    [SerializeField, Header("Range effect:")]
    private int range = 2;

    public override void action(BasicActor from, BasicActor to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.ArrowProof, buffsID.MidDefense);

        // TODO // Instantiate 

        // Apply to me, and others allies 
        foreach (Turnable actor in TurnManager.instance.attenders) {
            if (actor.CompareTag(from.tag) && (Stage.StageBuilder.getDistance(from.transform.position, actor.transform.position) <= range)) {
                ((Actor)to).buffs.applyBuffs((Actor)to, buffsID.ArrowProof, buffsID.MidDefense);
            }
        }
        from.endAction();
    }

}

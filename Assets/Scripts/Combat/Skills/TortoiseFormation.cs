using UnityEngine;

[CreateAssetMenu(fileName = "TortoiseFormation", menuName = "Combat/Skills/Tortoise Formation")]
public class TortoiseFormation : Skill {

    public override void action(BasicActor from, BasicActor to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.ArrowProof, buffsID.MidDefense);
        base.action(from, to);
    }

}

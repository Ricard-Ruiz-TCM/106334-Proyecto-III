using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "TortoiseFormation", menuName = "Combat/Skills/Tortoise Formation")]
public class TortoiseFormation : Skill {
    public EventReference formationSound;
    public override void action(BasicActor from, Node to) {
        FMODManager.instance.PlayOneShot(formationSound);
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.ArrowProof, buffsID.MidDefense);
        from.endAction();
    }

}

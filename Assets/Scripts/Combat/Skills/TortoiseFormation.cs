using UnityEngine;

[CreateAssetMenu(fileName = "TortoiseFormation", menuName = "Combat/Skills/Tortoise Formation")]
public class TortoiseFormation : Skill {
    public override void action(BasicActor from, Node to) {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.UsoHabilidad);
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.ArrowProof, buffsID.MidDefense);
        ((Actor)from).Anim.SetBool("deff", true);
        from.endAction();
    }

}

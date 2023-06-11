using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "Defense", menuName = "Combat/Skills/Defense")]
public class Defense : Skill 
{
    public override void action(BasicActor from, Node to) 
    {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.UsoHabilidad);
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.LowDefense);
        //((Actor)from).UseSkill(null);
        from.endAction();
    }

}

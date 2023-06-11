using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "Vanish", menuName = "Combat/Skills/Vanish")]
public class Vanish : Skill {
    public EventReference vanishSound;
    public override void action(BasicActor from, Node to) 
    {
        FMODManager.instance.PlayOneShot(vanishSound);
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.Invisible);
        from.endAction();
    }

}

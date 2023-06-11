using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "TrojanHorse", menuName = "Combat/Skills/Trojan Horse")]
public class TrojanHorse : Skill {
    public EventReference trojanSound;
    public override void action(BasicActor from, Node to) {
        FMODManager.instance.PlayOneShot(trojanSound);
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.Invencible, buffsID.MidMovement);
        from.endAction();
    }

}

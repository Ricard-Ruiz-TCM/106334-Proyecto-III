using UnityEngine;

[CreateAssetMenu(fileName = "TrojanHorse", menuName = "Combat/Skills/Trojan Horse")]
public class TrojanHorse : Skill {
    public override void action(BasicActor from, Node to) {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.TrojanHorse);
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.Invencible, buffsID.MidMovement);
        from.endAction();
    }

}

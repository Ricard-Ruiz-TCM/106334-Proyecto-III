using UnityEngine;

[CreateAssetMenu(fileName = "Defense", menuName = "Combat/Skills/Defense")]
public class Defense : Skill {

    public override void action(BasicActor from, BasicActor to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.LowDefense);
        from.endAction();
    }

}

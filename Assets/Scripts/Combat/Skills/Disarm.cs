using UnityEngine;

[CreateAssetMenu(fileName = "Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {

    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            ((Actor)target).buffs.applyBuffs((Actor)target, buffsID.Disarmed);
        }
        from.endAction();
    }

}

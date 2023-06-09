using UnityEngine;

[CreateAssetMenu(fileName = "AchillesHeel", menuName = "Combat/Skills/Achilles Heel")]
public class AchillesHeel : Skill {

    public override void action(BasicActor from, Node to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.MidDamage);
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            target.takeDamage((Actor)from, from.totalDamage());
        }
        from.endAction();
    }

}

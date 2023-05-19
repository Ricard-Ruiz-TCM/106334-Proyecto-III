using UnityEngine;

[CreateAssetMenu(fileName = "AchillesHeel", menuName = "Combat/Skills/Achilles Heel")]
public class AchillesHeel : Skill {

    public override void action(BasicActor from, BasicActor to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.MidDamage);
        from.endAction();
    }

}

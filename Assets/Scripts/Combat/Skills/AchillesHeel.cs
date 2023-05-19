using UnityEngine;

[CreateAssetMenu(fileName = "AchillesHeel", menuName = "Combat/Skills/Achilles Heel")]
public class AchillesHeel : Skill {

    public override void action(BasicActor from, BasicActor to) {
        base.action(from, to);
        //from.Status.ApplyStatus(buffsID.MidDamage);
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "AchillesHeel", menuName = "Combat/Skills/Achilles Heel")]
public class AchillesHeel : Skill {

    public override void Action(BasicActor from) {
        base.Action(from);
        //from.Status.ApplyStatus(buffsID.MidDamage);
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "AchillesHeel", menuName = "Combat/Skills/Achilles Heel")]
public class AchillesHeel : Skill {

    public override void Action(Turnable from) {
        base.Action(from);
        //from.Status.ApplyStatus(buffsID.MidDamage);
    }

}

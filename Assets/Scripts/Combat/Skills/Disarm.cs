using UnityEngine;

[CreateAssetMenu(fileName = "Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {

    public override void Action(Turnable from) {
        base.Action(from);
        Turnable to = null; // TODO // find the target
        //to.Status.ApplyStatus(buffsID.Disarmed);
    }

}

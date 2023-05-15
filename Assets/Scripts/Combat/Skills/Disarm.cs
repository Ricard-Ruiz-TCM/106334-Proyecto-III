using UnityEngine;

[CreateAssetMenu(fileName = "Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {

    public override void Action(Actor from) {
        base.Action(from);
        Actor to = null; // TODO // find the target
        to.Status.ApplyStatus(buffsID.Disarmed);
    }

}

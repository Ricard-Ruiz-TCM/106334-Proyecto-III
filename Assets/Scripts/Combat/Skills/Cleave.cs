using UnityEngine;

[CreateAssetMenu(fileName = "Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {

    public override void Action(Actor from) {
        base.Action(from);
        Actor to = null; // TODO // find the target
        to.Status.ApplyStatus(buffsID.Stunned);
    }

}

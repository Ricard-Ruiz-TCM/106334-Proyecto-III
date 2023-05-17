using UnityEngine;

[CreateAssetMenu(fileName = "Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {

    public override void Action(BasicActor from) {
        base.Action(from);
        Turnable to = null; // TODO // find the target
        //to.Status.ApplyStatus(buffsID.Stunned);
    }

}

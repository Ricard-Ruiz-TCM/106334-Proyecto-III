using UnityEngine;

[CreateAssetMenu(fileName = "Bloodlust", menuName = "Combat/Skills/Bloodlust")]
public class Bloodlust : Skill {

    public override void Action(Turnable from) {
        base.Action(from);
        Turnable to = null; // TODO // find the target
        //to.Status.ApplyStatus(buffsID.Bleeding);
    }

}

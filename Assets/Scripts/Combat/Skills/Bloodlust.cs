using UnityEngine;

[CreateAssetMenu(fileName = "Bloodlust", menuName = "Combat/Skills/Bloodlust")]
public class Bloodlust : Skill {

    public override void Action(Actor from) {
        base.Action(from);
        Actor to = null; // TODO // find the target
        to.Status.ApplyStatus(buffsID.Bleeding);
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "Vanish", menuName = "Combat/Skills/Vanish")]
public class Vanish : Skill {

    public override void Action(BasicActor from) {
        base.Action(from);
        //from.Status.ApplyStatus(buffsID.Invisible);
    }

}

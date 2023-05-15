using UnityEngine;

[CreateAssetMenu(fileName = "Defense", menuName = "Combat/Skills/Defense")]
public class Defense : Skill {

    public override void Action(Actor from) {
        base.Action(from);
        from.Status.ApplyStatus(buffsID.LowDefense);
    }

}

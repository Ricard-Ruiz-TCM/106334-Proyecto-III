using UnityEngine;

[CreateAssetMenu(fileName = "TortoiseFormation", menuName = "Combat/Skills/Tortoise Formation")]
public class TortoiseFormation : Skill {

    public override void Action(Actor from) {
        base.Action(from);
        from.Status.ApplyStatus(buffsID.ArrowProof);
        from.Status.ApplyStatus(buffsID.MidDefense);
    }

}

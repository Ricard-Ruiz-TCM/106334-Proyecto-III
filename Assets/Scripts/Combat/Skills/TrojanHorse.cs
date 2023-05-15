using UnityEngine;

[CreateAssetMenu(fileName = "TrojanHorse", menuName = "Combat/Skills/Trojan Horse")]
public class TrojanHorse : Skill {

    [Header("Special Mod:")]
    public int extraMovility;

    public override void Action(Actor from) {
        base.Action(from);
        from.Status.ApplyStatus(buffsID.Invencible);
        from.Status.ApplyStatus(buffsID.MidMovement);
    }

}

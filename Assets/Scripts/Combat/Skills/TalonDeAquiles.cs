using UnityEngine;

[CreateAssetMenu(fileName = "new Talon", menuName = "Combat/Skills/TalonDeAquiles")]
public class TalonDeAquiles : Skill {

    public override void Special(Actor from) {
        from.Status.ApplyStatus(buffsnDebuffs.DamageBuffx3);
        from.EndAction();
    }

}

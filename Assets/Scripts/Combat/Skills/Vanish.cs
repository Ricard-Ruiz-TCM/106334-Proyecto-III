using UnityEngine;

[CreateAssetMenu(fileName = "Vanish", menuName = "Combat/Skills/Vanish")]
public class Vanish : Skill {

    public override void action(BasicActor from, BasicActor to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.Invisible);
        base.action(from, to);
    }

}

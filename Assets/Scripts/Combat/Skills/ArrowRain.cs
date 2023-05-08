using UnityEngine;

[CreateAssetMenu(fileName = "new ArrowRain", menuName = "Combat/Skills/Arrow Rain")]
public class ArrowRain : Skill {

    public override void Special(Actor from) {
        CombatManager.instance.UseSkill(from, range, skill, from.canInteract);
    }

}

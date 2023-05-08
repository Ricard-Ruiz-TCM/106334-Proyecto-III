using UnityEngine;

[CreateAssetMenu(fileName = "new Attack", menuName = "Combat/Skills/Attack")]
public class Attack : Skill {

    public override void Special(Actor from) {
        FindObjectOfType<CombatManager>().UseSkill(from, _range, _skill, from.canInteract);

    }

}

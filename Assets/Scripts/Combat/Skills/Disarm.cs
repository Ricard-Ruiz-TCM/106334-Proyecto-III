using UnityEngine;

[CreateAssetMenu(fileName = "new Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {

    public override void Special(Actor from) {
        CombatManager.instance.UseSkill(from, range, skill, from.canInteract);
        Debug.Log("Disarm special attack");
    }

}

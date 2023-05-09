using UnityEngine;

[CreateAssetMenu(fileName = "new Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {

    public override void Special(Actor from) {
        CombatManager.instance.UseSkill(from, range, skill, from.canInteract);
        Debug.Log("Cleave special attack");
    }

}

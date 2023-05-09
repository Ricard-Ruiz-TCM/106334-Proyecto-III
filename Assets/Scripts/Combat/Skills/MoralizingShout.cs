using UnityEngine;

[CreateAssetMenu(fileName = "new MoralizingShout", menuName = "Combat/Skills/Moralizing Shout")]
public class MoralizingShout : Skill {

    public override void Special(Actor from) {
        CombatManager.instance.UseSkill(from, range, skill, from.canInteract);
        from.EndAction();
        Debug.Log("MoralizingShout special attack");
    }

}

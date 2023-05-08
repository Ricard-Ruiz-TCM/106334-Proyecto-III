using UnityEngine;

[CreateAssetMenu(fileName = "new MoralizingShout", menuName = "Combat/Skills/Moralizing Shout")]
public class MoralizingShout : Skill {

    public override void Special(Actor from) 
    {
        FindObjectOfType<CombatManager>().UseSkill(from, _range, _skill, from.canInteract);
        Debug.Log("MoralizingShout special attack");
    }

}

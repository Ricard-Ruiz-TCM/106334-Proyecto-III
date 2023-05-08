using UnityEngine;

[CreateAssetMenu(fileName = "new Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {

    public override void Special(Actor from) 
    {
        FindObjectOfType<CombatManager>().UseSkill(from, _range, _skill, from.canInteract);
        Debug.Log("Cleave special attack");
    }

}

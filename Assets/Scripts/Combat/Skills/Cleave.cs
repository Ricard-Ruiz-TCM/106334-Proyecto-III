using UnityEngine;

[CreateAssetMenu(fileName = "new Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {

    public override void Special(Actor from) 
    {
        FindObjectOfType<CombatManager>().GolpeDemoledor(from, range,skill);
        Debug.Log("Cleave special attack");
    }

}

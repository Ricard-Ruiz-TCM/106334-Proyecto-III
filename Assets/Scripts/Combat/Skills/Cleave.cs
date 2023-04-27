using UnityEngine;

[CreateAssetMenu(fileName = "new Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill {

    public override void Special(Actor from) 
    {
        FindObjectOfType<CombatManager>().GolpeDemoledor(from, _range,_skill);
        Debug.Log("Cleave special attack");
    }

}

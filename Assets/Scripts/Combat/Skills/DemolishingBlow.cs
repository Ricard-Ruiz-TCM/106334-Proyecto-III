using UnityEngine;

[CreateAssetMenu(fileName = "new DemolishingBlow", menuName = "Combat/Skills/Demolishing Blow")]
public class DemolishingBlow : Skill {

    public override void Special(Actor from) {
        //FindObjectOfType<CombatManager>().GolpeDemoledor(from, duration, skill);
        //Debug.Log("Cleave special attack");
    }

}

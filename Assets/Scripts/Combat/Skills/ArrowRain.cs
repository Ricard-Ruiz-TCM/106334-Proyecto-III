using UnityEngine;

[CreateAssetMenu(fileName = "new ArrowRain", menuName = "Combat/Skills/Arrow Rain")]
public class ArrowRain : Skill {

    public override void Special(Actor from) {
        FindObjectOfType<CombatManager>().ProvaArco(from, _range);
        Debug.Log("ArrowRain special attack");
    }

}

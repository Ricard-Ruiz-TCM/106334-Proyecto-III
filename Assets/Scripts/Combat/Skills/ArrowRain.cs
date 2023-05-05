using UnityEngine;

[CreateAssetMenu(fileName = "new ArrowRain", menuName = "Combat/Skills/Arrow Rain")]
public class ArrowRain : Skill {

    public override void Special(Actor from) {
        FindObjectOfType<CombatManager>().Arco(from, duration, skill);
        Debug.Log("ArrowRain special attack");
    }

}

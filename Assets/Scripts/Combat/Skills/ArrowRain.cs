using UnityEngine;

[CreateAssetMenu(fileName = "new ArrowRain", menuName = "Combat/Skills/Arrow Rain")]
public class ArrowRain : Skill {

    public override void Special(Actor from) {
        FindObjectOfType<CombatManager>().UseSkill(from, _range, _skill,from.canInteract);
        Debug.Log("ArrowRain special attack");
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "new ArrowRain", menuName = "Combat/Skills/Arrow Rain")]
public class ArrowRain : Skill {

    public override void Special() {
        Debug.Log("ArrowRain special attack");
    }

}

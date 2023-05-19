using UnityEngine;

[CreateAssetMenu(fileName = "ArrowRain", menuName = "Combat/Skills/ArrowRain")]
public class ArrowRain : Skill {

    public override void action(BasicActor from, BasicActor to) {
        Debug.Log("APPLY ARROW");
    }

}

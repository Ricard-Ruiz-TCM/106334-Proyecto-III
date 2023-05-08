using UnityEngine;

[CreateAssetMenu(fileName = "new Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {

    public override void Special(Actor from) {
        Debug.Log("Disarm special attack");
    }

}

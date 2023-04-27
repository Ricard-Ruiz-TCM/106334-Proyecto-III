using UnityEngine;

[CreateAssetMenu(fileName = "new MoralizingShout", menuName = "Combat/Skills/Moralizing Shout")]
public class MoralizingShout : Skill {

    public override void Special(Actor from) {
        Debug.Log("MoralizingShout special attack");
    }

}

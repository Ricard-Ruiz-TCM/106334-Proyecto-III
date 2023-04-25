using UnityEngine;

[CreateAssetMenu(fileName = "new Stun", menuName = "Combat/Skills/Stun")]
public class Stun : Skill {

    public override void Special() {
        Debug.Log("Stun special attack");
    }

}

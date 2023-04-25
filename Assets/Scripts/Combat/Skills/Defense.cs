using UnityEngine;

[CreateAssetMenu(fileName = "new Defense", menuName = "Combat/Skills/Defense")]
public class Defense : Skill {

    public override void Special() {
        Debug.Log("Defense special attack");
    }

}

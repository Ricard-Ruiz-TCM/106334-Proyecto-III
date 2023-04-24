using UnityEngine;

[CreateAssetMenu(fileName = "new Attack", menuName = "Combat/Attack")]
public class Attack : Skill {

    public override void Special() {
        Debug.Log("Attack special attack");
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "new Stab", menuName = "Combat/Stab")]
public class Stab : Skill {

    public override void Special() {
        Debug.Log("Stab special attack");
    }

}

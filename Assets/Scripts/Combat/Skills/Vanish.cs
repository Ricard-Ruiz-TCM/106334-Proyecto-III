using UnityEngine;

[CreateAssetMenu(fileName = "new Vanish", menuName = "Combat/Vanish")]
public class Vanish : Skill {

    public override void Special() {
        Debug.Log("Vanish special attack");
    }

}

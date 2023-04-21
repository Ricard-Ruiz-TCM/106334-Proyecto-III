using UnityEngine;

[CreateAssetMenu(fileName = "new DoubleLunge", menuName = "Combat/Double Lunge")]
public class DoubleLunge : Skill {

    public override void Special() {
        Debug.Log("DoubleLunge special attack");
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "new MoralizingShout", menuName = "Combat/Moralizing Shout")]
public class MoralizingShout : Skill {

    public override void Special() {
        Debug.Log("MoralizingShout special attack");
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "new Defense", menuName = "Combat/Skills/Defense")]
public class Defense : Skill {

    public override void Special(Actor from) {
        from.AddTempDef(_defenseMod);

        Debug.Log("Defense special attack");
    }

}

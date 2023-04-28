using UnityEngine;

[CreateAssetMenu(fileName = "new Defense", menuName = "Combat/Skills/Defense")]
public class Defense : Skill {

    public override void Special(Actor from) {
        from.AddTempDef(defenseMod);
        from.EndAction();

        Debug.Log("Defense special attack");
    }

}

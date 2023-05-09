using UnityEngine;

[CreateAssetMenu(fileName = "new Defense", menuName = "Combat/Skills/Defense")]
public class Defense : Skill {

    public override void Special(Actor from) 
    {
        from.Status.ApplyStatus(buffsnDebuffs.NormalDefense);
        from.EndAction();
        Debug.Log("Defense special attack");
    }

}

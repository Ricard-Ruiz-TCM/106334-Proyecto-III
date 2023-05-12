using UnityEngine;

[CreateAssetMenu(fileName = "new Heal", menuName = "Combat/Skills/Heal")]
public class Heal : Skill
{
    public override void Special(Actor from)
    {
        from.Status.ApplyStatus(buffsnDebuffs.Heal);
        from.EndAction();
        Debug.Log("heal");
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "new SedDeSangre", menuName = "Combat/Skills/SedDeSangre")]
public class SedDeSangre : Skill
{
    public override void Special(Actor from)
    {
        CombatManager.instance.UseSkill(from, range, skill, from.canInteract);
        from.Status.ApplyStatus(buffsnDebuffs.HealPerHit);
    }
}

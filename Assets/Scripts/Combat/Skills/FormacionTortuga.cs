using UnityEngine;
[CreateAssetMenu(fileName = "new Tortuga", menuName = "Combat/Skills/FormacionTortuga")]
public class FormacionTortuga : Skill {
    public override void Special(Actor from) {
        from.Status.ApplyStatus(buffsnDebuffs.ArrowInmune);
        from.Status.ApplyStatus(buffsnDebuffs.DefenseBuffx3);
        from.EndAction();
    }
}

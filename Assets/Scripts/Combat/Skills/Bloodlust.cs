using UnityEngine;

[CreateAssetMenu(fileName = "Bloodlust", menuName = "Combat/Skills/Bloodlust")]
public class Bloodlust : Skill {

    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            target.takeDamage((Actor)from, from.totalDamage());
            from.heal(target.damageTaken());
        }
        from.endAction();
    }

}

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Double Lungue", menuName = "Combat/Skills/Double Lungue")]
public class DoubleLungue : Skill {

    [SerializeField]
    private float _damageDelay = 0.25f;

    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            from.StartCoroutine(C_DamageAgain(from, target));
        }
        from.endAction();
    }

    /** Coroutine para aplicar el segúndo hit un tiempo después */
    public IEnumerator C_DamageAgain(BasicActor from, BasicActor target) {
        target.takeDamage((Actor)from, from.totalDamage());
        yield return new WaitForSeconds(_damageDelay);
        if (from != null)
            target.takeDamage((Actor)from, from.totalDamage());
    }

}

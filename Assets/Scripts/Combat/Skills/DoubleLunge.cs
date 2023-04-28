using UnityEngine;

[CreateAssetMenu(fileName = "new DoubleLunge", menuName = "Combat/Skills/Double Lunge")]
public class DoubleLunge : Skill {

    public override void Special(Actor from) {


        /** Actor to = _combat.TryGetActor(-..);

         to.TakeDamage(from.Damage());*/
        FindObjectOfType<CombatManager>().Lanza(from, range, skill);
        Debug.Log("DoubleLunge special attack");
    }

}

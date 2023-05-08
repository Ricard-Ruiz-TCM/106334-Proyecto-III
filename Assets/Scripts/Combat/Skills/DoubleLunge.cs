using UnityEngine;

[CreateAssetMenu(fileName = "new DoubleLunge", menuName = "Combat/Skills/Double Lunge")]
public class DoubleLunge : Skill {

    public override void Special(Actor from) {


        /** Actor to = _combat.TryGetActor(-..);

         to.TakeDamage(from.Damage());*/

        FindObjectOfType<CombatManager>().UseSkill(from, _range, _skill, from.canInteract);
        
        Debug.Log("DoubleLunge special attack");
    }

}

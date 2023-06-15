using UnityEngine;

[CreateAssetMenu(fileName = "Vanish", menuName = "Combat/Skills/Vanish")]
public class Vanish : Skill {

    BoxCollider _collider;

    public override void action(BasicActor from, Node to) {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.Vanish);
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.Invisible);
        ((Actor)from).Anim.Play("Vanish");
        from.GetComponent<WeaponHolder>().invisible();
        from.endAction();
    }

}

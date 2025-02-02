﻿using UnityEngine;

[CreateAssetMenu(fileName = "Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.Disarm);
            ((Actor)target).buffs.applyBuffs((Actor)target, buffsID.Disarmed);

            Vector3 relativePos = from.transform.position - target.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            var lookPos = target.transform.position - from.transform.position;
            lookPos.y = 0;
            from.transform.rotation = Quaternion.LookRotation(lookPos);
            target.GetComponent<WeaponHolder>().disarm();
        } else {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.MissAttack);
        }
        ((Actor)from).Anim.Play("Disarm");
        from.endAction();
    }

}

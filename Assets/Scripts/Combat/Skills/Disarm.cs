using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) 
    {
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.Disarm);
            ((Actor)target).buffs.applyBuffs((Actor)target, buffsID.Disarmed);

            Vector3 relativePos = from.transform.position - target.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            if (!target.GetComponent<StaticActor>())
            {
                GameObject blood = Instantiate(bloodPrefab, new Vector3(target.transform.position.x, target.transform.position.y + 0.8f, target.transform.position.z), rotation);
                Destroy(blood, 2f);
            }
            var lookPos = target.transform.position - from.transform.position;
            lookPos.y = 0;
            from.transform.rotation = Quaternion.LookRotation(lookPos);
        }
        else
        {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.MissAttack);
        }
        from.endAction();
    }

}

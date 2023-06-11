using UnityEngine;
using System.Collections;
using FMODUnity;

[CreateAssetMenu(fileName = "Double Lungue", menuName = "Combat/Skills/Double Lungue")]
public class DoubleLungue : Skill {
    [SerializeField]
    private float _damageDelay = 0.25f;
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.DoubleLungue);
            from.StartCoroutine(C_DamageAgain(from, target));
            var lookPos = target.transform.position - from.transform.position;

            Vector3 relativePos = from.transform.position - target.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            //GameObject blood = Instantiate(bloodPrefab, new Vector3(target.transform.position.x, target.transform.position.y + 0.8f, target.transform.position.z), rotation);
            //Destroy(blood, 2f);
            lookPos.y = 0;
            from.transform.rotation = Quaternion.LookRotation(lookPos);
        }
        else
        {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.DoubleMiss);
        }
        from.endAction();
    }

    /** Coroutine para aplicar el segúndo hit un tiempo después */
    public IEnumerator C_DamageAgain(BasicActor from, BasicActor target) {
        target.takeDamage((Actor)from, from.totalDamage());
        yield return new WaitForSeconds(_damageDelay);
        target.takeDamage((Actor)from, (int)((float)from.totalDamage() / 2f));
    }

}

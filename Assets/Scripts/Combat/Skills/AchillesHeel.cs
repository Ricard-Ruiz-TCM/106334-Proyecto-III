using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;

[CreateAssetMenu(fileName = "AchillesHeel", menuName = "Combat/Skills/Achilles Heel")]
public class AchillesHeel : Skill {
    public List<AchilesSlash> slashes;
    public EventReference abilitySound;
    public List<EventReference> soundEvents;
    public EventReference missAttack;
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {
        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.MidDamage);
        FMODManager.instance.PlayOneShot(abilitySound);
        from.StartCoroutine(StartSlash(from));
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            var equipment = from.gameObject.GetComponent<EquipmentManager>();
            switch (equipment.weapon.ID)
            {
                case itemID.Bow:
                    FMODManager.instance.PlayOneShot(soundEvents[0]);
                    FMODManager.instance.PlayOneShot(soundEvents[1]);
                    break;
                case itemID.Dolabra:
                    FMODManager.instance.PlayOneShot(soundEvents[2]);
                    break;
                case itemID.Gladius:
                    FMODManager.instance.PlayOneShot(soundEvents[3]);
                    break;
                case itemID.Hasta:
                    FMODManager.instance.PlayOneShot(soundEvents[4]);
                    break;
                case itemID.Pugio:
                    FMODManager.instance.PlayOneShot(soundEvents[5]);
                    break;
            }
            target.takeDamage((Actor)from, from.totalDamage());
            var lookPos = target.transform.position - from.transform.position;

            Vector3 relativePos = from.transform.position - target.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            GameObject blood = Instantiate(bloodPrefab, new Vector3(target.transform.position.x, target.transform.position.y + 0.8f, target.transform.position.z), rotation);
            Destroy(blood, 2f);

            lookPos.y = 0;
            from.transform.rotation = Quaternion.LookRotation(lookPos);
        }
        else
        {
            FMODManager.instance.PlayOneShot(missAttack);
        }

        from.endAction();
    }

    IEnumerator StartSlash(BasicActor from)
    {
        GameObject go;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < slashes.Count; i++)
        {
            go = Instantiate(slashes[i].objSlash, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(from.transform);
            go.transform.localPosition = slashes[i].pos;
            go.transform.localRotation = Quaternion.identity;
            go.SetActive(true);
            yield return new WaitForSeconds(slashes[i].delay);
            Destroy(go);
        }
        DissableSlashes();
    }
    void DissableSlashes()
    {
        for (int i = 0; i < slashes.Count; i++)
        {
            slashes[i].objSlash.SetActive(false);
        }
    }

}
[System.Serializable]
public class AchilesSlash
{
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}

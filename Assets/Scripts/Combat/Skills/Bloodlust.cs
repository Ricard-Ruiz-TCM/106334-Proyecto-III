using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bloodlust", menuName = "Combat/Skills/Bloodlust")]
public class Bloodlust : Skill {
    public List<BloodSlash> slashes;
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        FMODManager.instance.PlayOneShot(FMODEvents.instance.Bloodlust);
        from.StartCoroutine(StartSlash(from));
        if (target != null) {
            var equipment = from.gameObject.GetComponent<EquipmentManager>();
            switch (equipment.weapon.ID) {
                case itemID.Bow:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.InicioLanzarFlecha);
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.FlechaContraCarne);
                    break;
                case itemID.Dolabra:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.DolabraContraCarne);
                    break;
                case itemID.Gladius:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.GladiusContraCarne);
                    break;
                case itemID.Hasta:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.HastaContraCarne);
                    break;
                case itemID.Pugio:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.PugioContraCarne);
                    break;
                default:
                    break;
            }
            target.takeDamage((Actor)from, from.totalDamage(), ((Actor)from).equip.weapon.ID);
            from.heal(target.damageTaken());
            from.entitieUI.GetComponent<EntitieUI>().SetHeal((float)target.damageTaken() / (float)from.maxHealth());
            var lookPos = target.transform.position - from.transform.position;

            Vector3 relativePos = from.transform.position - target.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            if (!target.GetComponent<StaticActor>()) {
                GameObject blood = Instantiate(bloodPrefab, new Vector3(target.transform.position.x, target.transform.position.y + 0.8f, target.transform.position.z), rotation);
                Destroy(blood, 2f);
            }

            lookPos.y = 0;
            from.transform.rotation = Quaternion.LookRotation(lookPos);
        } else {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.MissAttack);
        }

        from.endAction();
    }
    IEnumerator StartSlash(BasicActor from) {
        GameObject go;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < slashes.Count; i++) {
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
    void DissableSlashes() {
        for (int i = 0; i < slashes.Count; i++) {
            slashes[i].objSlash.SetActive(false);
        }
    }
}
[System.Serializable]
public class BloodSlash {
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Combat/Skills/Attack")]
public class Attack : Skill {
    public List<Slash> slashes;
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float heightY;
    [SerializeField] float duration;
    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);

        if (target != null) {

            WeaponItem weapon = ((Actor)from).equip.weapon;
            if (target is PilarStatic) {
                switch (weapon.ID) {
                    case itemID.Bow:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.InicioLanzarFlecha);
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.FlechaPiedra);
                        from.StartCoroutine(ShootArrow(from, target));
                        break;
                    case itemID.Dolabra:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.DolabraPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Gladius:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.GladiusPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Hasta:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.HastaPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Pugio:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.PugioPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    default:
                        break;
                }
            } else {
                switch (weapon.ID) {
                    case itemID.Bow:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.InicioLanzarFlecha);
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.FlechaContraCarne);
                        from.StartCoroutine(ShootArrow(from, target));
                        break;
                    case itemID.Dolabra:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.DolabraContraCarne);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Gladius:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.GladiusContraCarne);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Hasta:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.HastaContraCarne);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Pugio:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.PugioContraCarne);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    default:
                        break;
                }
            }

            target.takeDamage((Actor)from, from.totalDamage(), ((Actor)from).equip.weapon.ID);
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

        ((Actor)from).endAction();
    }
    IEnumerator ShootArrow(BasicActor from, BasicActor target)
    {
        yield return new WaitForSeconds(1f);
        GameObject arrow = Instantiate(arrowPrefab, new Vector3(from.transform.position.x, from.transform.position.y + 1.7f, from.transform.position.z), Quaternion.identity);
        float timer = 0;
        while (timer< duration)
        {
            //float linearT = timer / duration;
            //float heightT = curve.Evaluate(linearT);

            //float height = Mathf.Lerp(0, heightY, heightT);

            //arrow.transform.position = Vector3.Lerp(arrow.transform.position, targetPos, linearT) + new Vector3(0, height, 0);

            arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y + curve.Evaluate(Time.deltaTime * 3), arrow.transform.position.z);

            yield return null;
        }
        
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
public class Slash {
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}

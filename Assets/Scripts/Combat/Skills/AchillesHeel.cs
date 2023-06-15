using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchillesHeel", menuName = "Combat/Skills/Achilles Heel")]
public class AchillesHeel : Skill {
    public List<AchilesSlash> slashes;
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float duration;
    public override void action(BasicActor from, Node to) 
    {
        var lookPos = Stage.StageBuilder.getGridPlane(to).position - from.transform.position;
        lookPos.y = 0;
        from.transform.rotation = Quaternion.LookRotation(lookPos);

        int rValue = Random.Range(0, 2); // 1 vertical

        ((Actor)from).buffs.applyBuffs((Actor)from, buffsID.MidDamage);
        FMODManager.instance.PlayOneShot(FMODEvents.instance.UsoHabilidad);
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            var equipment = from.gameObject.GetComponent<EquipmentManager>();
            switch (equipment.weapon.ID) {
                case itemID.Bow:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.InicioLanzarFlecha);
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.FlechaContraCarne);
                    ((Actor)from).Anim.Play("Bow");
                    from.StartCoroutine(ShootArrow(from, target.transform.position, target));
                    break;
                case itemID.Dolabra:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.DolabraContraCarne);
                    ((Actor)from).Anim.Play("Attack" + rValue.ToString());
                    from.StartCoroutine(StartSlash(from, rValue, target));
                    break;
                case itemID.Gladius:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.GladiusContraCarne);
                    ((Actor)from).Anim.Play("Attack" + rValue.ToString());
                    from.StartCoroutine(StartSlash(from, rValue, target));
                    break;
                case itemID.Hasta:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.HastaContraCarne);
                    ((Actor)from).Anim.Play("Attack" + rValue.ToString());
                    from.StartCoroutine(StartSlash(from, rValue, target));
                    break;
                case itemID.Pugio:
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.PugioContraCarne);
                    ((Actor)from).Anim.Play("Attack" + rValue.ToString());
                    from.StartCoroutine(StartSlash(from, rValue,target));
                    break;
                default:
                    break;
            }



        } else {
            WeaponItem weapon = ((Actor)from).equip.weapon;
            if (weapon.ID == itemID.Bow)
            {
                ((Actor)from).Anim.Play("Bow");
                from.StartCoroutine(ShootArrow(from, Stage.StageBuilder.getGridPlane(to).position,null));
            }
            else
            {
                ((Actor)from).Anim.Play("Attack" + rValue.ToString());
                from.StartCoroutine(StartSlash(from, rValue,null));
            }
            FMODManager.instance.PlayOneShot(FMODEvents.instance.MissAttack);
        }

        from.endAction();
    }
    IEnumerator ShootArrow(BasicActor from, Vector3 target, BasicActor targetActor)
    {
        yield return new WaitForSeconds(1f);
        GameObject arrow = Instantiate(arrowPrefab, new Vector3(from.transform.position.x, from.transform.position.y + 1.7f, from.transform.position.z), Quaternion.identity);
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float percentageDuration = timer / duration;
            arrow.transform.position = Vector3.Slerp(new Vector3(from.transform.position.x, from.transform.position.y + 1.7f, from.transform.position.z), new Vector3(target.x, target.y + 1f, target.z), percentageDuration);
            arrow.transform.rotation = Quaternion.LookRotation(target - from.transform.position);
            yield return null;
        }
        if (targetActor != null)
        {
            targetActor.takeDamage((Actor)from, from.totalDamage(), ((Actor)from).equip.weapon.ID);
            Vector3 relativePos = from.transform.position - targetActor.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            if (!targetActor.GetComponent<StaticActor>())
            {
                GameObject blood = Instantiate(bloodPrefab, new Vector3(targetActor.transform.position.x, targetActor.transform.position.y + 0.8f, targetActor.transform.position.z), rotation);
                Destroy(blood, 2f);
            }
        }
        Destroy(arrow);
    }

    IEnumerator StartSlash(BasicActor from, int num, BasicActor targetActor)
    {
        GameObject go;
        yield return new WaitForSeconds(0.4f);
        if (num == 1)
        {
            go = Instantiate(slashes[1].objSlash, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(from.transform);
            go.transform.localPosition = slashes[1].pos;
            go.transform.localRotation = Quaternion.identity;
            go.SetActive(true);
            if (targetActor != null)
            {
                targetActor.takeDamage((Actor)from, from.totalDamage(), ((Actor)from).equip.weapon.ID);
                Vector3 relativePos = from.transform.position - targetActor.transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                if (!targetActor.GetComponent<StaticActor>())
                {
                    GameObject blood = Instantiate(bloodPrefab, new Vector3(targetActor.transform.position.x, targetActor.transform.position.y + 0.8f, targetActor.transform.position.z), rotation);
                    Destroy(blood, 2f);
                }
            }
            yield return new WaitForSeconds(slashes[1].delay);
            Destroy(go);
        }
        else if (num == 0)
        {
            go = Instantiate(slashes[0].objSlash, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(from.transform);
            go.transform.localPosition = slashes[0].pos;
            go.transform.localRotation = Quaternion.identity;
            go.SetActive(true);
            if (targetActor != null)
            {
                targetActor.takeDamage((Actor)from, from.totalDamage(), ((Actor)from).equip.weapon.ID);
                Vector3 relativePos = from.transform.position - targetActor.transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                if (!targetActor.GetComponent<StaticActor>())
                {
                    GameObject blood = Instantiate(bloodPrefab, new Vector3(targetActor.transform.position.x, targetActor.transform.position.y + 0.8f, targetActor.transform.position.z), rotation);
                    Destroy(blood, 2f);
                }
            }
            yield return new WaitForSeconds(slashes[0].delay);
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
public class AchilesSlash {
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}

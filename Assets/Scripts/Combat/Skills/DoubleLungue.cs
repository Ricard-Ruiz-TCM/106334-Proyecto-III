using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Lungue", menuName = "Combat/Skills/Double Lungue")]
public class DoubleLungue : Skill {
    public List<AchilesSlash> slashes;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField]
    private float _damageDelay = 0.25f;
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] float duration;
    public override void action(BasicActor from, Node to) {

        var lookPos = Stage.StageBuilder.getGridPlane(to).position - from.transform.position;
        lookPos.y = 0;
        from.transform.rotation = Quaternion.LookRotation(lookPos);

        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {

            WeaponItem weapon = ((Actor)from).equip.weapon;
            if (weapon.ID == itemID.Bow)
            {

                from.StartCoroutine(ShootArrow(from, target.transform.position,target));
            }
            else
            {
                from.StartCoroutine(StartSlash(from,target));
            }
            FMODManager.instance.PlayOneShot(FMODEvents.instance.DoubleLungue);
            //from.StartCoroutine(C_DamageAgain(from, target));
            
        } else {
            WeaponItem weapon = ((Actor)from).equip.weapon;
            if (weapon.ID == itemID.Bow)
            {               
                from.StartCoroutine(ShootArrow(from, Stage.StageBuilder.getGridPlane(to).position,null));
            }
            else
            {
                
                from.StartCoroutine(StartSlash(from,null));
            }
            FMODManager.instance.PlayOneShot(FMODEvents.instance.DoubleMiss);
        }
        from.endAction();
    }

    IEnumerator ShootArrow(BasicActor from, Vector3 target,BasicActor targetActor)
    {
        float timer = 0;
        for (int i = 0; i < 2; i++)
        {
            ((Actor)from).Anim.Play("Bow");
            yield return new WaitForSeconds(1f);
            GameObject arrow = Instantiate(arrowPrefab, new Vector3(from.transform.position.x, from.transform.position.y + 1.7f, from.transform.position.z), Quaternion.identity);
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
            timer = 0;
            yield return new WaitForSeconds(1f);
        }
        
    }

    IEnumerator StartSlash(BasicActor from, BasicActor targetActor)
    {
        int rValue = Random.Range(0, 2); // 1 vertical
        GameObject go;
        ((Actor)from).Anim.Play("Attack" + rValue.ToString());
        yield return new WaitForSeconds(0.4f);
        if (rValue == 1)
        {
            for (int i = 0; i < 2; i++)
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
                if(i == 0)
                {
                    ((Actor)from).Anim.Play("Attack" + rValue.ToString());
                }
                
            }
           
        }
        else if (rValue == 0)
        {
            for (int i = 0; i < 2; i++)
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
                if (i == 0)
                {
                    ((Actor)from).Anim.Play("Attack" + rValue.ToString());
                }
            }
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
public class DobleSlash
{
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}


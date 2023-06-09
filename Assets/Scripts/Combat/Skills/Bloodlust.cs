using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Bloodlust", menuName = "Combat/Skills/Bloodlust")]
public class Bloodlust : Skill {
    public List<BloodSlash> slashes;
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        from.StartCoroutine(StartSlash(from));
        if (target != null) {
            target.takeDamage((Actor)from, from.totalDamage());
            from.heal(target.damageTaken());
            var lookPos = target.transform.position - from.transform.position;

            Vector3 relativePos = from.transform.position - target.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            GameObject blood = Instantiate(bloodPrefab, new Vector3(target.transform.position.x, target.transform.position.y + 0.8f, target.transform.position.z), rotation);
            Destroy(blood, 2f);

            lookPos.y = 0;
            from.transform.rotation = Quaternion.LookRotation(lookPos);
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
public class BloodSlash
{
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}

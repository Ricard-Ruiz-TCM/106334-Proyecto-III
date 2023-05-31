using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Attack", menuName = "Combat/Skills/Attack")]
public class Attack : Skill {

    public List<Slash> slashes;

    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        Debug.Log("33333");
        from.StartCoroutine(StartSlash(from));
        if (target != null)
        {
            target.takeDamage((Actor)from, from.totalDamage());
        }
        ((Actor)from).endAction();
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
public class Slash
{
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}

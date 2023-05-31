using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LowDefense", menuName = "Combat/Buffs/Low Defense")]
public class LowDefense : ModBuff {
    public GameObject shieldPrefab;
    public GameObject shieldObj;

    public override void onApply(BasicActor me) 
    {
        shieldObj = Instantiate(shieldPrefab, Vector3.zero, Quaternion.identity);
        shieldObj.transform.SetParent(me.transform);
        shieldObj.transform.localPosition = new Vector3(0.203f, 0.87f, 0.1f);
        Debug.Log("TODO: Apply Invisible Feedback + extras.");
    }

    public override void onRemove(BasicActor me) {
        shieldObj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("hit");
        Destroy(shieldObj, 0.7f);
        Debug.Log("TODO: Remove Invisible Feedback");
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn Invisible Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn Invisible Feedback");
    }

}

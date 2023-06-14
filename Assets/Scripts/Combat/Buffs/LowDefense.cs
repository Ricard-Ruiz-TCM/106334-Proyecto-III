using UnityEngine;

[CreateAssetMenu(fileName = "LowDefense", menuName = "Combat/Buffs/Low Defense")]
public class LowDefense : ModBuff {
    public GameObject shieldPrefab;
    public GameObject shieldObj;

    public override void onApply(BasicActor me) {
        shieldObj = Instantiate(shieldPrefab, Vector3.zero, Quaternion.identity);
        shieldObj.transform.SetParent(me.transform);
        shieldObj.transform.localPosition = new Vector3(0.203f, 0.87f, 0.1f);
    }

    public override void onRemove(BasicActor me) {
        if (shieldObj != null) {
            shieldObj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("hit");
            Destroy(shieldObj, 0.7f);
        }
        ((Actor)me).Anim.SetBool("deff", false);
    }

    public override void startTurnEffect(BasicActor me) {
    }

    public override void endTurnEffect(BasicActor me) {
    }

}

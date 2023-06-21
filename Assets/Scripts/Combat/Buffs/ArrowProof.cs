using UnityEngine;

[CreateAssetMenu(fileName = "ArrowProof", menuName = "Combat/Buffs/ArrowProof")]
public class ArrowProof : Buff 
{
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject shieldVFX;

    public override void onApply(BasicActor me) 
    {
        Debug.Log("TODO: Apply ArrowProof Feedback");
        shieldVFX = Instantiate(shieldPrefab, Vector3.zero, Quaternion.identity);
        shieldVFX.transform.parent = me.transform;
        shieldVFX.transform.localPosition = new Vector3(0, 0, 0.734f);
        shieldVFX.transform.localRotation = Quaternion.Euler(0, 90, 0);
    }

    public override void onRemove(BasicActor me) {
        Debug.Log("TODO: Remove ArrowProof Feedback");
        shieldVFX.GetComponent<Animator>().SetTrigger("noBuff");
        Destroy(shieldVFX, 0.5f);
    }

    public override void startTurnEffect(BasicActor me) {
        Debug.Log("TODO: Start Turn ArrowProof Feedback");
    }

    public override void endTurnEffect(BasicActor me) {
        Debug.Log("TODO: End Turn ArrowProof Feedback");
    }

}

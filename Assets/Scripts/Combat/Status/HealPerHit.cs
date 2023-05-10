using UnityEngine;
[CreateAssetMenu(fileName = "new HealPerHit", menuName = "Combat/Status/HealPerHit")]
public class HealPerHit : Status
{
    public override void Effect(Actor me)
    {
        //me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = materialDefault;
    }
}

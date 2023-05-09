using UnityEngine;
[CreateAssetMenu(fileName = "new Invencibility", menuName = "Combat/Status/Invencibility")]
public class Invencibility : Status
{

    [SerializeField, Header("Material:")]
    private Material materialDefault;
    public override void Effect(Actor me)
    {
        me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = materialDefault;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "new Invisible", menuName = "Combat/Status/Invisible")]
public class Invisible : Status {

    [SerializeField, Header("Material:")]
    private Material materialDefault;

    public override void Effect(Actor me) {
        me.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = materialDefault;
    }

}

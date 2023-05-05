using UnityEngine;

[CreateAssetMenu(fileName = "new Invisible", menuName = "Combat/Status/Invisible")]
public class Invisible : Status {

    public override void Effect(Actor me) {
        Debug.Log("Invisible Effect");
    }

}

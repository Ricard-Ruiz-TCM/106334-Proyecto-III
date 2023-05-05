using UnityEngine;

[CreateAssetMenu(fileName = "new Stunned", menuName = "Combat/Status/Stunned")]
public class Stunned : Status {

    public override void Effect(Actor me) {
        Debug.Log("Stunned Effect");
    }

}

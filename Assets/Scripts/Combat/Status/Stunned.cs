using UnityEngine;

[CreateAssetMenu(fileName = "new Stunned", menuName = "Combat/Status/Stunned")]
public class Stunned : Status {

    public override void Effect(Actor me) {
        me.EndAction();
        me.EndMovement();
    }

}

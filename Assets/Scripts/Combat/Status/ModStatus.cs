using UnityEngine;

[CreateAssetMenu(fileName = "new ModStatus", menuName = "Combat/Status/Modification Status")]
public class ModStatus : Status {

    public modificationType type;
    public int modification;
    public modificationOperations operationType;

    public override void Effect(Actor me) { }

}



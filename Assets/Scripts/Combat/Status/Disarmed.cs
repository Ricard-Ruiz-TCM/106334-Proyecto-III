using UnityEngine;

[CreateAssetMenu(fileName = "new Disarmed", menuName = "Combat/Status/Disarmed")]
public class Disarmed : Status
{
    public override void Effect(Actor me)
    {
        me.EndAction();
    }

}

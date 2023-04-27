using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new EndRoadEvent", menuName = "Dialogue/Custom/End Road Event")]
public class EndRoadEvent : DialogueTrigger
{

    public static event Action endRoadEvent;

    public override void Trigger()
    {
        endRoadEvent?.Invoke();
    }

}

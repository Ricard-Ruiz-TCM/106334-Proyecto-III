using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new EndRoadEvent", menuName = "Dialog/Custom/End Road Event")]
public class EndRoadEvent : DialogTrigger {

    public static event Action endRoadEvent;

    public override void Trigger() {
        endRoadEvent?.Invoke();
    }

}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OpenPerkPanel", menuName = "Dialog/Custom/Open Perk Panel")]
public class OpenPerkPanel : DialogTrigger {

    public static event Action openPerkPanel;

    public override void Trigger() {
        openPerkPanel?.Invoke();
    }

}

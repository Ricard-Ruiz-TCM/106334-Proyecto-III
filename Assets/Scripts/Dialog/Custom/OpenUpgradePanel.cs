using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OpenUpgradePanel", menuName = "Dialog/Custom/Open Upgrade Panel")]
public class OpenUpgradePanel : DialogTrigger {

    public static event Action openUpgradePanel;

    public override void Trigger() {
        openUpgradePanel?.Invoke();
    }

}

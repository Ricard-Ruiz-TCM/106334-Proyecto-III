using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OnOpenUpgradePanel", menuName = "Dialogue/Open Upgrade Panel Node")]
public class OnOpenUpgradePanel : DialogTrigger {

    public static event Action onTrigger;

    public override void Trigger() {
        onTrigger?.Invoke();
    }

}
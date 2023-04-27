using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OpenUpgradePanel", menuName = "Dialogue/Custom/Open Upgrade Panel")]
public class OpenUpgradePanel : DialogueTrigger
{

    public static event Action openUpgradePanel;

    public override void Trigger()
    {
        openUpgradePanel?.Invoke();
    }

}

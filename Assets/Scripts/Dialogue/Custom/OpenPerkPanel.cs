using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OpenPerkPanel", menuName = "Dialogue/Custom/Open Perk Panel")]
public class OpenPerkPanel : DialogueTrigger
{

    public static event Action openPerkPanel;

    public override void Trigger()
    {
        openPerkPanel?.Invoke();
    }

}

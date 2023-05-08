using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OnOpenPerkPanel", menuName = "Dialogue/Open Perk Panel Node")]
public class OnOpenPerkPanel : DialogTrigger {

    public static event Action onTrigger;

    public override void Trigger() {
        onTrigger?.Invoke();
    }

}
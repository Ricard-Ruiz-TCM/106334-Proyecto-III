using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OnEndDialog", menuName = "Dialogue/End Dialog Node")]
public class OnEndDialog : DialogTrigger {

    public static event Action onTrigger;

    public override void Trigger() {
        onTrigger?.Invoke();
    }

}
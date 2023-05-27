using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OnNextStageDialog", menuName = "Dialogue/Next Stage Dialog Node")]
public class OnNextStageDialog : DialogTrigger {

    public static event Action onTrigger;

    public override void Trigger() {
        onTrigger?.Invoke();
    }

}
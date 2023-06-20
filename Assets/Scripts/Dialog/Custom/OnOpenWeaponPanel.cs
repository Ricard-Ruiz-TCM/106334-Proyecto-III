using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OnOpenWeaponPanel", menuName = "Dialogue/Open Weapon Panel Node")]
public class OnOpenWeaponPanel : DialogTrigger {

    public static event Action onTrigger;

    public override void Trigger() {
        onTrigger?.Invoke();
    }

}
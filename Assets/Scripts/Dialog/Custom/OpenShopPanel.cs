using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OpenShopPanel", menuName = "Dialog/Custom/Open Shop Panel")]
public class OpenShopPanel : DialogTrigger {

    public static event Action openShopPanel;

    public override void Trigger() {
        openShopPanel?.Invoke();
    }

}

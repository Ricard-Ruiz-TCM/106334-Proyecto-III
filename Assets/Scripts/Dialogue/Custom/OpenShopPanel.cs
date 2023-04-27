using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new OpenShopPanel", menuName = "Dialogue/Custom/Open Shop Panel")]
public class OpenShopPanel : DialogueTrigger
{

    public static event Action openShopPanel;

    public override void Trigger()
    {
        openShopPanel?.Invoke();
    }

}

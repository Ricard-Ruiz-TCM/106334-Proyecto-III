using System;
using UnityEngine;

[Serializable]
public class ShieldInventoryItem {

    public ShieldItem shield;
    [Range(0, 2)]
    public int upgrade;

    public int defense => shield.defense[upgrade];

}
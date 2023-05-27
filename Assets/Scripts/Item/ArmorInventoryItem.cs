using System;
using UnityEngine;

[Serializable]
public class ArmorInventoryItem {

    public ArmorItem armor;
    [Range(0,2)]
    public int upgrade;

    public int defense => armor.defense[upgrade];

}


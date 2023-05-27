using System;
using UnityEngine;

[Serializable]
public class WeaponInventoryItem {

    public WeaponItem weapon;
    [Range(0, 2)]
    public int upgrade;

    public int damage => weapon.damage[upgrade];

}

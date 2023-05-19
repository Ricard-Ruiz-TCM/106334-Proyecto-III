using System;

[Serializable]
public class WeaponInventoryItem {

    public WeaponItem weapon;
    public int upgrade;

    public int damage => weapon.damage[upgrade];

}

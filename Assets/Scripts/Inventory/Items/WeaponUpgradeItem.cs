using System;

[Serializable]
public class WeaponUpgradeItem {
    public WeaponItem weapon;
    public int level;

    public float Damage() {
        return weapon._damage[level];
    }
}

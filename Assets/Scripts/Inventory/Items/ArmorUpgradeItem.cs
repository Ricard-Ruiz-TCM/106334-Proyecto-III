using System;

[Serializable]
public class ArmorUpgradeItem {
    public ArmorItem armor;
    public int level;

    public int Defense() {
        return armor._defense[level];
    }
}

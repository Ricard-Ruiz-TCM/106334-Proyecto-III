using System;

[Serializable]
public class ShieldInventoryItem {

    public ShieldItem shield;
    public int upgrade;

    public int defense => shield.defense[upgrade];

}
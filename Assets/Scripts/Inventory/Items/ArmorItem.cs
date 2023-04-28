using UnityEngine;

[CreateAssetMenu(fileName = "new ArmorItem", menuName = "Items/Armor Item")]
public class ArmorItem : Item {

    [Header("Armor Stats:")]
    public int health;
    public int[] defense;

}

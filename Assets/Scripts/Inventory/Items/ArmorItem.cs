using UnityEngine;

[CreateAssetMenu(fileName = "new ArmorItem", menuName = "Items/Armor Item")]
public class ArmorItem : Item {

    [Header("Armor Stats:")]
    public int[] defense;
    public int movement;
    public itemID toughness;
    public itemID weakness;

}

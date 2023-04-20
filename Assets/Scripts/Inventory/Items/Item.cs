using UnityEngine;

public enum items {
    // Weapons
    gladiusWeapon, 
    // Armors
    leatherArmor,
    // Quests
    quest0,
    // Books
    book0
}

[CreateAssetMenu(fileName = "new Item", menuName = "Items/Item")]
public class Item : ScriptableObject {

    public items _id;
    public Sprite _icon;
    public string _keyName;
    public string _keyDescription;

}

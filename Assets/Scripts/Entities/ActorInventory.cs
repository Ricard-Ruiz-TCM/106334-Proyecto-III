using UnityEngine;

public class ActorInventory : ActorManager {

    [SerializeField, Header("Equipment:")]
    protected ArmorItem _armor;
    public ArmorItem Armor() {
        return _armor;
    }
    [SerializeField]
    protected WeaponItem _weapon;
    public WeaponItem Weapon() {
        return _weapon;
    }
    [SerializeField]
    protected ShieldItem _shield;
    public ShieldItem Shield() {
        return _shield;
    }

    [SerializeField, Header("Inventory:")]
    protected Inventory _inventory;

}

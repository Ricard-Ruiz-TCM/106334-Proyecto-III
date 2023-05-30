using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    [SerializeField]
    protected ArmorInventoryItem _armor;
    public ArmorItem armor => _armor.armor;

    [SerializeField]
    protected WeaponInventoryItem _weapon;
    public WeaponItem weapon => _weapon.weapon;

    [SerializeField]
    protected ShieldInventoryItem _shield;
    public ShieldItem shield => _shield.shield;

    public int damage => _weapon.damage;
    public int armorDefense => _armor.defense;
    public int shieldDefense => _shield.defense;

    public void SetEquipment(ArmorInventoryItem ar, WeaponInventoryItem wp, ShieldInventoryItem sh) {
        _armor = ar;
        _weapon = wp;
        _shield = sh;
    }

    public ArmorInventoryItem getArmorInvItem() {
        return _armor;
    }

    public WeaponInventoryItem getWeaponInvItem() {
        return _weapon;
    }

    public ShieldInventoryItem getShieldInvItem() {
        return _shield;
    }

}
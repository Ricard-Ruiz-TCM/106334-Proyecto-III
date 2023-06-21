using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectorUI : MonoBehaviour {


    /** Tenemos la perk seleciconada */
    private bool _selected = false;

    [SerializeField]
    private WeaponSelectorUI _other1, _other2;

    [SerializeField, Header("Weapon:")]
    private WeaponItem _weaponItem;

    [SerializeField]
    private UIText _txtName, _txtDesc;

    [SerializeField, Header("SelectButton:")]
    private Button _btn;

    public void SetWeapon(WeaponItem weaponItem) {
        _weaponItem = weaponItem;
        updateTexts();
    }

    public void updateTexts() {
        _txtName.SetKey(_weaponItem.keyName);
        _txtDesc.SetKey(_weaponItem.keyDescription);
    }

    public void BTN_SelectWeapon() {
        _btn.interactable = true;
        select();
        _other1.deSelect();
        _other2.deSelect();
    }

    public void select() {
        _selected = true;
    }

    public void deSelect() {
        _selected = false;
    }

    public void BTN_IsSelected() {
        if (_selected) {
            //WeaponInventoryItem wp = new WeaponInventoryItem();
            //wp.weapon = _weaponItem;
            //GameObject.FindGameObjectWithTag("EquipmentManager").GetComponent<EquipmentManager>().SetInventoryWeapon(wp);
            uCore.GameManager.getPlayer().setWeapon(_weaponItem);
        }
    }

}

using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour {

    public Image _icon;
    public UIText _name;
    public UIText _amount;
    public Button _usable;

    public void Set(InventoryItem item, Inventory inventory) {
        _icon.sprite = item.item._icon;
        _name.SetKey(item.item._keyName);
        _amount.UpdateText(item.amount);
        _usable.interactable = (item.item is UsableItem);

        // onClick
        if (_usable.interactable) {
            _usable.onClick.AddListener(() => { 
                inventory.UseItem(item.item._item);
            });
        }
    }
}
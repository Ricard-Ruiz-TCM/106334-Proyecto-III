using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour {

    public Image _icon;
    public UIText _name;
    public UIText _amount;

    public void Set(InventoryItem item, Inventory inventory) {
        _icon.sprite = item.item.icon;
        _name.SetKey(item.item.keyName);
        _amount.UpdateText(item.amount);
    }
}
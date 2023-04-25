using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory {

    // Callback 
    public Action<Inventory> onUpdateInventory;

    [SerializeField]
    private List<InventoryItem> _items;
    public List<InventoryItem> AllItems() {
        return _items;
    }

    public Inventory(params items[] itemsList) {
        _items = new List<InventoryItem>();

        foreach (items item in itemsList) {
            _items.Add(new InventoryItem() { item = uCore.GameManager.GetItem(item) });
        }
    }

    public void AddItem(items item, int am = 1) {
        Item it = uCore.GameManager.GetItem(item);
        int pos = Find(it);
        if (pos != -1) {
            _items[pos].amount += am;
        } else {
            _items.Add(new InventoryItem() { item = it, amount = am });
        }
        onUpdateInventory?.Invoke(this);
    }

    public void RemoveItem(items item) {
        int pos = Find(uCore.GameManager.GetItem(item));
        if (pos != -1) {
            if (_items[pos].amount > 1) {
                _items[pos].amount--;
            } else {
                _items.RemoveAt(pos);
            }
            onUpdateInventory?.Invoke(this);
        }
    }

    public void UseItem(items item) {
        Item it = uCore.GameManager.GetItem(item);
        int pos = Find(it);
        if (pos != -1) {
            if (it is UsableItem) {
                ((UsableItem)it).Use();
                RemoveItem(item);
                onUpdateInventory?.Invoke(this);
            }
        }
    }

    public bool Contains(items item) {
        return (Find(uCore.GameManager.GetItem(item)) != -1);
    }

    private int Find(Item item) {
        for (int i = 0; i < _items.Count; i++) {
            if (_items[i].item.Equals(item)) {
                return i;
            }
        }
        return -1;
    }

}

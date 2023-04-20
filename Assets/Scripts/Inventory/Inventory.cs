using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory {

    [SerializeField]
    private List<InventoryItem> _items;

    public Inventory(params Item[] items) {
        _items = new List<InventoryItem>();

        foreach (Item item in items) {
            _items.Add(new InventoryItem() { item = item });
        }
    }

    public void AddItem(Item item) {
        int pos = Find(item);
        if (pos != -1) {
            _items[pos].amount++;
        } else {
            _items.Add(new InventoryItem() { item = item });
        }
    }

    public void RemoveItem(Item item) {
        int pos = Find(item);
        if (pos != -1) {
            if (_items[pos].amount > 1) {
                _items[pos].amount--;
            } else {
                _items.RemoveAt(pos);
            }
        }
    }

    public void UseItem(Item item) {
        int pos = Find(item);
        if (pos != -1) {
            if (item is UsableItem) {
                ((UsableItem)item).Use();
                RemoveItem(item);
            }
        }
    }

    public bool Contains(Item item) {
        return (Find(item) != -1);
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

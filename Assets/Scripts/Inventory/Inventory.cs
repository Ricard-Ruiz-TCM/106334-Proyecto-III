using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory {

    // Callback 
    public Action<Inventory> onUpdateInventory;

    [SerializeField]
    private List<InventoryItem> _items;
    public List<InventoryItem> Items => _items;

    /** Constructor */
    public Inventory(params items[] itemsList) {
        _items = new List<InventoryItem>();
        foreach (items item in itemsList) {
            _items.Add(new InventoryItem() { item = uCore.GameManager.GetItem(item) });
        }
    }

    /** Añadir un item al inventario o aumenta su cantidad */
    public void AddItem(items item, int amount = 1) {
        Item itemData = uCore.GameManager.GetItem(item);
        if (Contains(item)) {
            _items[Find(itemData)].amount += amount;
        } else {
            _items.Add(new InventoryItem() { item = itemData, amount = amount });
        }
        onUpdateInventory?.Invoke(this);
    }

    /** Elimina un item del inventario o disminute su cantidad */
    public void RemoveItem(items item) {
        if (Contains(item)) {
            int pos = Find(uCore.GameManager.GetItem(item));
            if (_items[pos].amount > 1) {
                _items[pos].amount--;
            } else {
                _items.RemoveAt(pos);
            }
            onUpdateInventory?.Invoke(this);
        }
    }

    /** Check si tenemos este item en el iventario */
    public bool Contains(items item) {
        return (Find(uCore.GameManager.GetItem(item)) != -1);
    }

    /** Busca la posición del item | -1 -> no encontrado */
    private int Find(Item item) {
        for (int i = 0; i < _items.Count; i++) {
            if (_items[i].item.Equals(item)) {
                return i;
            }
        }
        return -1;
    }

}

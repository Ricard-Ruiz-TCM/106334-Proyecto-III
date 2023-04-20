﻿using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField, Header("Prefab")]
    private GameObject _inventoryItemUI;

    public void UpdateInventory(Inventory inventory) {
        ClearInventory();

        foreach(InventoryItem iit in inventory.AllItems()) {
            GameObject.Instantiate(_inventoryItemUI, transform).GetComponent<InventoryItemUI>().Set(iit, inventory);
        }
    }

    public void ClearInventory() {
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

}

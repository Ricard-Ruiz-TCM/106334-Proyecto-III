using UnityEngine;

public class InventoryUI : MonoBehaviour {

    [SerializeField, Header("Prefab")]
    private GameObject _inventoryItemUI;


    public void AsignInventory(Inventory inv) {
        inv.onUpdateInventory += UpdateInventory;
    }

    public void UpdateInventory(Inventory inventory) {
        ClearInventory();

        foreach (InventoryItem iit in inventory.AllItems()) {
            GameObject.Instantiate(_inventoryItemUI, transform).GetComponent<InventoryItemUI>().Set(iit, inventory);
        }
    }

    public void ClearInventory() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

}

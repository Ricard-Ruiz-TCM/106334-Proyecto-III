using UnityEngine;

public class ManualTurnActor : ManualTurnable {

    [SerializeField]
    private Inventory _inventory;

    private void Awake() {
        _inventory.onUpdateInventory += FindObjectOfType<InventoryUI>().UpdateInventory;
    }

    void Start() {
        SubscribeManager();
    }

    public override void Act() {
        base.Act();

        _inventory.AddItem(items.Gladius);
        _inventory.AddItem(items.Book0);
        _inventory.AddItem(items.Leather);

        Invoke("EndAction", 3f);
    }

    public override bool CanAct() {
        return uCore.Action.GetKeyDown(KeyCode.Space) && acting.Equals(progress.ready);
    }

    public override void Move() {
        base.Move();

        _inventory.AddItem(items.Leather);
        _inventory.AddItem(items.Leather);
        _inventory.AddItem(items.Leather);
        _inventory.AddItem(items.Leather);
        _inventory.AddItem(items.Leather);

        Invoke("EndMovement", 3f);
    }

    public override bool CanMove() {
        return uCore.Action.GetKeyDown(KeyCode.A) && moving.Equals(progress.ready);
    }

}

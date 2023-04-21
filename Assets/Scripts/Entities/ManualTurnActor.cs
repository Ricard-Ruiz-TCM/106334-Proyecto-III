using UnityEngine;

public class ManualTurnActor : ManualTurnable {

    [SerializeField]
    private Inventory _inventory;

    void Start() {
        SubscribeManager();
    }

    public override void Act() {
        base.Act();

        _inventory.AddItem(Items.Book1);
        _inventory.AddItem(Items.Book1);
        _inventory.RemoveItem(Items.WeaponGladius);

        Invoke("EndAction", 3f);
    }

    public override bool CanAct() {
        return uCore.Action.GetKeyDown(KeyCode.Space) && acting.Equals(progress.ready);
    }

    public override void Move() {
        base.Move();

        _inventory.AddItem(Items.WeaponGladius);
        _inventory.UseItem(Items.Book1);
        _inventory.UseItem(Items.Book1);
        _inventory.UseItem(Items.Book1);
        _inventory.RemoveItem(Items.Book1);
        _inventory.RemoveItem(Items.Book1);
        _inventory.RemoveItem(Items.Book1);
        _inventory.RemoveItem(Items.Book1);

        Invoke("EndMovement", 3f);
    }

    public override bool CanMove() {
        return uCore.Action.GetKeyDown(KeyCode.A) && moving.Equals(progress.ready);
    }

}

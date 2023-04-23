using UnityEngine;

public abstract class Actor : MonoBehaviour, ITurnable {

    [SerializeField, Header("Statistics:")]
    protected Statistics _statistics;

    [SerializeField, Header("Equipment:")]
    protected ArmorItem _armor;
    [SerializeField]
    protected WeaponItem _weapon;

    [SerializeField, Header("Inventory:")]
    protected Inventory _inventory;

    #region ITurnable 
    public progress moving {
        get; private set;
    }
    public progress acting {
        get; private set;
    }
    public bool hasTurnEnded {
        get; private set;
    }

    /** Métodos para entrar/salir al sistema de turnos */
    protected void SubscribeManager() {
        FindObjectOfType<TurnManager>().Add(this);
    }
    protected void UnSubscribeManger() {
        FindObjectOfType<TurnManager>().Remove(this);
    }

    /** Métodos de control del turno **/
    public void BeginTurn() {
        moving = progress.ready;
        acting = progress.ready;
        hasTurnEnded = false;
    }
    public void EndTurn() {
        hasTurnEnded = true;
    }

    /** Método de control de Acción */
    public abstract bool CanAct();
    public virtual void Act() {
        StartAct();
    }
    protected void StartAct() {
        acting = progress.doing;
    }
    protected void EndAction() {
        acting = progress.done;
    }

    /** Método de control de Movimiento */
    public abstract bool CanMove();
    public virtual void Move() {
        StartMove();
    }
    protected void StartMove() {
        moving = progress.doing;
    }
    protected void EndMovement() {
        moving = progress.done;
    }
    #endregion

}
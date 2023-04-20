using System.Collections;
using UnityEngine;

public abstract class ManualTurnable : MonoBehaviour, ITurnable {

    // ITurnable
    public progress moving { get; private set; }
    public progress acting { get; private set; }
    public bool hasTurnEnded { get; private set; }

    protected void SubscribeManager() {
        FindObjectOfType<TurnManager>().Add(this);
    }

    protected void UnSubscribeManger() {
        FindObjectOfType<TurnManager>().Remove(this);
    }

    public void BeginTurn() {
        moving = progress.ready;
        acting = progress.ready;
        hasTurnEnded = false;
    }

    public void EndTurn() {
        hasTurnEnded = true;
    }

    // Acting
    public abstract bool CanAct();
    public virtual void Act() { StartAct(); }
    protected void StartAct() { acting = progress.doing; }
    protected void EndAction() { acting = progress.done; }

    // Movement
    public abstract bool CanMove(); 
    public virtual void Move() { StartMove(); }
    protected void StartMove() { moving = progress.doing; }
    protected void EndMovement() { moving = progress.done; }

}

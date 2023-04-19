using System.Collections;
using UnityEngine;

public abstract class ManualTurnable : MonoBehaviour, ITurnable {

    // ITurnable
    public progress moving { get; set; }
    public progress acting { get; set; }
    public bool hasTurnEnded { get; set; }

    protected void SubscribeManager() {
        FindObjectOfType<TurnManager>().Add(this);
    }

    public void BeginTurn() {
        moving = progress.ready;
        moving = progress.ready;
        hasTurnEnded = false;
    }

    public void EndTurn() {
        hasTurnEnded = true;
    }

    // Acting
    public abstract void Act();
    public abstract bool CanAct();
    protected void StartAct() { acting = progress.doing; }
    protected void EndAction() { acting = progress.done; }

    // Movement
    public abstract void Move();
    public abstract bool CanMove(); 
    protected void StartMove() { moving = progress.doing; }
    protected void EndMovement() { moving = progress.done; }

}

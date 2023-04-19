using System.Collections;
using UnityEngine;

public abstract class AutomaticTurnable : MonoBehaviour, IAutomaticTurnable {
    
    // ITurnable
    public bool hasMoved { get; set; }
    public bool hasActed { get; set; }
    public bool hasTurnEnded { get; set; }
    
    // IAutomaticTurnable
    public progress moving { get; set; }
    public progress acting { get; set; }

    protected void AddToManager() {
        FindObjectOfType<TurnManager>().Add(this);
    }

    public void Act() {
        acting = progress.doing;
        StartCoroutine(C_Act());
    }
    public abstract IEnumerator C_Act();

    public bool CanAct() {
        if (moving.Equals(progress.doing)) return false;
        return (!hasActed && acting.Equals(progress.waiting));
    }

    public bool CanMove() {
        if (acting.Equals(progress.doing)) return false;
        return (!hasMoved && moving.Equals(progress.waiting));
    }

    public void BeginTurn() {
        hasMoved = false;
        hasMoved = false;
        hasTurnEnded = false;
        moving = progress.waiting;
        acting = progress.waiting;
    }

    public void EndTurn() {
        hasTurnEnded = true;
    }

    public void Move() {
        moving = progress.doing;
        StartCoroutine(C_Move());
    }

    public abstract IEnumerator C_Move();

    public void EndMovement() {
        moving = progress.done;
        hasMoved = true;
    }

    public void EndAction() {
        acting = progress.done;
        hasActed = true;
    }

}

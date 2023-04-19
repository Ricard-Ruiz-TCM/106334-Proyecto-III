using System.Collections;
using UnityEngine;

public abstract class ManualTurnable : MonoBehaviour, ITurnable {

    // ITurnable
    public bool hasMoved { get; set; }
    public bool hasActed { get; set; }
    public bool hasTurnEnded { get; set; }

    protected void AddToManager() {
        FindObjectOfType<TurnManager>().Add(this);
    }

    public void BeginTurn() {
        hasMoved = false;
        hasMoved = false;
        hasTurnEnded = false;
    }

    public void EndTurn() {
        hasTurnEnded = true;
    }

    public abstract bool CanAct();
    public abstract void Act();

    public abstract bool CanMove(); 
    public abstract void Move();

}

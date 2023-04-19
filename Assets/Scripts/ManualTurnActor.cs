using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTurnActor : MonoBehaviour, ITurnable
{
    public bool isAutomatic { get; set; }

    public bool hasMoved { get; set; }

    public bool hasActed { get; set; }

    public bool hasEnded { get; set; }

    // Unity Awake
    void Awake() {
        isAutomatic = false;
        hasMoved = false;
        hasMoved = false;
        hasEnded = false;
    }

    void Start() {
        FindObjectOfType<TurnManager>().Add(this);
    }

    public void Act() {
        Debug.Log("ACTOR MANUAL TURN ACT");
    }

    public bool CanAct() {
        return true;
    }

    public bool CanMove() {
        return true;
    }

    public void EndTurn() {
        Debug.Log("ACTOR MANUAL END TURN");
    }

    public void Move() {
        Debug.Log("ACTOR MANUAL TURN MOVE");
    }
}

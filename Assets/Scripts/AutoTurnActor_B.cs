using UnityEngine;

public class AutoTurnActor_B : MonoBehaviour, ITurnable {

    public bool isAutomatic { get; set; }

    public bool hasMoved { get; set; }

    public bool hasActed { get; set; }

    public bool hasTurnEnded { get; set; }

    public progress moving { get; set; }

    public progress acting { get; set; }

    // Unity Awake
    void Awake() {
        isAutomatic = true;
        hasMoved = false;
        hasMoved = false;
        hasTurnEnded = false;
        moving = progress.waiting;
        acting = progress.waiting;
    }

    void Start() {
        FindObjectOfType<TurnManager>().Add(this);
    }

    public void Act() {
        Debug.Log("ACTOR B TURN ACT");
    }

    public bool CanAct() {
        return true;
    }

    public bool CanMove() {
        return true;
    }

    public void EndTurn() {
        Debug.Log("ACTOR B END TURN");
    }

    public void Move() {
        Debug.Log("ACTOR B TURN MOVE");
    }
}

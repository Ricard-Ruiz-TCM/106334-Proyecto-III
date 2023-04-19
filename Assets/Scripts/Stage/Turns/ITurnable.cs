using System.Collections;

public interface ITurnable {

    public bool hasMoved { get; }
    public bool hasActed { get; }
    public bool hasTurnEnded { get; }

    public bool CanMove();
    public void Move();

    public bool CanAct();
    public void Act();

    public void BeginTurn();
    public void EndTurn();

}

public interface IAutomaticTurnable : ITurnable {

    public progress moving { get; }
    public progress acting { get; }

    public IEnumerator C_Move();
    public IEnumerator C_Act();

}
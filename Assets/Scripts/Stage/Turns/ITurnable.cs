


public interface ITurnable {

    public bool isAutomatic { get; }

    public bool hasMoved { get; }
    public progress moving { get; }
    public bool hasActed { get; }
    public progress acting { get; }
    public bool hasTurnEnded { get; }

    public bool CanMove();
    public void Move();

    public bool CanAct();
    public void Act();

    public void EndTurn();

}

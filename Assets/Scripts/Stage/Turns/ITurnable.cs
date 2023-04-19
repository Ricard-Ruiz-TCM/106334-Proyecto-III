
public interface ITurnable {

    public bool isAutomatic { get; }

    public bool hasMoved { get; }
    public bool hasActed { get; }
    public bool hasEnded { get; }

    public bool CanMove();
    public void Move();

    public bool CanAct();
    public void Act();

    public void EndTurn();

}

public interface ITurnable {

    public progress moving { get; }
    public progress acting { get; }
    public bool hasTurnEnded { get; }

    public bool CanMove();
    public void Move();

    public bool CanAct();
    public void Act();

    public void BeginTurn();
    public void EndTurn();

}

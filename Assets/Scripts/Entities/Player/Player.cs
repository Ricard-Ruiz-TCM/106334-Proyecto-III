
public struct PlayerStates {
    public static PlayerIddle Iddle;
    public static PlayerMove Move;
    public static PlayerAttack Attack;
    public static PlayerDie Die;
}

public class Player : Actor {

    protected override void ConstructMachine() {

        PlayerStates.Iddle = GetComponent<PlayerIddle>();
        PlayerStates.Move = GetComponent<PlayerMove>();
        PlayerStates.Attack = GetComponent<PlayerAttack>();
        PlayerStates.Die = GetComponent<PlayerDie>();

        StateMachine.LoadStates();

        StateMachine.InnitialState = PlayerStates.Iddle;
    }

}
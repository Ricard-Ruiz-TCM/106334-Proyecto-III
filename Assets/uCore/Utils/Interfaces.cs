
// Interface para control de los estados de la FStateMachine y los objetos BasicState
public interface IState {
    public void OnEnter();
    public void OnState();
    public void OnExit();
}
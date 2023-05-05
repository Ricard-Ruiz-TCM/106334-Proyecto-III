
/** interface IState
 * -----------------
 * 
 * Interfaz para el control de métodos "básicos" de un estado
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v2.0 (04/2023)
 * 
 */

public interface IState {

    /** Core */
    public string Name {
        get; set;
    }
    public state Status {
        get; set;
    }
    public FStateMachine StateMachine {
        get; set;
    }

    /** OnEnter */
    public void OnEnter();
    /** OnState */
    public void OnState();
    /** OnExit */
    public void OnExit();

}
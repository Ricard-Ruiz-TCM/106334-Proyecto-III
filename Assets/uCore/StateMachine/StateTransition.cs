
/** class StateTransition
 * ----------------------
 * 
 * Clase encargada de gestionar las transiciones entre estados
 * Contiene la condición a modo de Action y un trigger adicional
 * Funciona con delegados propios 
 * 
 * @see BasicState
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v1.0 (02/2023)
 * 
 */

public class StateTransition {

    /** Delegods: Definición */
    public delegate bool TCD(); // Condition
    public delegate void TTD(); // Triger

    /** Delegados: Declaración */
    private TCD _condition;
    private TTD _trigger;

    /** Destiny */
    private BasicState _destiny;
    public BasicState Next() {
        return _destiny;
    }

    /** Contructor
     * @param TCD condition Callback de condicion
     * @param TTD trigger Callback de trigger */
    public StateTransition(TCD condition, TTD trigger = null) {
        _condition = condition;
        _trigger = trigger;
    }

    /** Método SetNext
     * Establece el destiny de la transición
     * @param BasicState destiny */
    public void SetNext(BasicState destiny) {
        _destiny = destiny;
    }

    /** Método CheckTransition
     * Comprueba si se ha realizado al condición de salida
     * @return bool true -> Se ha cumplido false -> no se ha cumplido */
    public bool CheckTransition() {
        return _condition();
    }
    /** Método OnTrigger
     * Ejecuta el callback de _trigger */
    public void OnTrigger() {
        _trigger?.Invoke();
    }

}

using UnityEngine;

/** abstract class StateActor
 * --------------------------
 * 
 * Estructura para gestionar los Actores de un juego
 * Pensada para ser el "contenedor" de la FStateMachine
 * Puede crear transiciones y añadirlas a la maquina principal
 * Gestiona el Awake, Start y Update de unity
 * 
 * @see FStateMachine
 * @see StateTransition
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v4.0 (04/2023)
 * 
 */

[RequireComponent(typeof(FStateMachine))]
public abstract class StateActor : MonoBehaviour
{

    /** FSM */
    protected FStateMachine StateMachine;

    /** Método abstracto ConstructMachine
     * Inicializa todo lo relacionado con la FSM */
    protected abstract void ConstructMachine();

    // Unity Awake
    protected void Awake()
    {
        StateMachine = GetComponent<FStateMachine>();
        ConstructMachine();
    }

    // Unity Start
    protected void Start()
    {
        StateMachine.StartMachine();
    }

    // Unity Update
    protected void Update()
    {
        StateMachine.UpdateMachine();
    }

    /** Método CreateTransitions
     * Crea una transición y la añade al sistema de estado/transisión de la FSM
     * @param BasicState from Estado origen
     * @param StateTransition.TCD condition Delegado para controlar la condición de cambio de estado
     * @param StateTransition.TTD trigger Delegado para controlar el "in between", se ejecuta entre el OnExit y el OnEnter del cambiod estado
     * @param BasicState to Estado destino */
    protected void CreateTransition(BasicState from, BasicState to, StateTransition.TCD condition, StateTransition.TTD trigger = null)
    {
        from.AddTransition(new StateTransition(condition, trigger), to);
    }

    /** Método AddTransition
     * Añadire directamente la la transición a la FSM
     * @param BasicState from Estado origen
     * @param StateTransition transition Transición ya creada y configurada, solo lista para añadirse al estado
     * @param BasicState to Estado destino */
    protected void AddTransition(BasicState from, StateTransition transition, BasicState to)
    {
        from.AddTransition(transition, to);
    }

}

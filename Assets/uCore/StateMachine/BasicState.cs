using System.Collections.Generic;
using UnityEngine;

/** abstract class BasicState
 * --------------------------
 * 
 * Control de un estado simple, este tiene su comprotamiento definido en IState
 * Controla sus propias transiciones y tiene acceso a la mauqina de estados
 * 
 * @see StateTransition
 * @see FStateMachine
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v4.0 (04/2023)
 * */

public abstract class BasicState : MonoBehaviour, IState {

    /** IState */
    [HideInInspector]
    public string Name {
        get; set;
    }
    [HideInInspector]
    public status Status {
        get; set;
    }
    [HideInInspector]
    public FStateMachine StateMachine {
        get; set;
    }

    /** Timer control */
    protected float TimeActive;
    protected float TimeInactive;

    /** Transiciones */
    public List<StateTransition> Transitions;

    /** Método abstract CreateTransitions
     * para crear transiciones de este estado concreto */
    public abstract void CreateTransitions();

    /** Métodos abstract de IState */
    public abstract void OnEnter();
    public abstract void OnState();
    public abstract void OnExit();

    /** Métodos AciveTime & InactiveTime & ResetTime
     * Control del tiempo de los estados, para saber el tiempo activa e inactivo
     * los métodos son publicos pero controlados principalmente por la FSM */
    public void ActiveTime() {
        TimeActive += Time.deltaTime;
    }
    public void InactiveTime() {
        TimeInactive += Time.deltaTime;
    }
    public void ResetTime() {
        TimeActive = 0f;
        TimeInactive = 0f;
    }

    /** Método AddTransition
     * Añade una transición a la lista de estados/transiciones
     * @param StateTransition transition Transicion ya creada
     * @param BasicState next Estado destino */
    public void AddTransition(StateTransition transition, BasicState next) {
        if (Transitions == null)
            Transitions = new List<StateTransition>();
        transition.SetNext(next);
        Transitions.Add(transition);
    }

}

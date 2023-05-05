using System;
using System.Collections.Generic;
using UnityEngine;

/** class StateMachine
 * --------------------
 * 
 * Maquina de estaados finita y completa
 * Los estados funcionan con el sistema de componentes, la máquina añade diferentes niveles de seguridad
 * Los estados son independientes ycontrolan las transiciones
 * La maquina hace los "Update"
 * 
 * @see StateTransition
 * @see BasicState
 * @see status
 * @see fsmSecurity
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v4.0 (02/2023)
 * 
 */

public class FStateMachine : MonoBehaviour {

    /** State Core */
    [Header("Status & Security:")]
    public state Status;
    public fsmSecurity Security;

    /** Relevant BasicStates */
    [HideInInspector]
    public BasicState InnitialState;
    [SerializeField, Header("Current State:")]
    private BasicState CurrentState;

    /** All BasicStates */
    [SerializeField, Header("All States:")]
    private List<BasicState> States;

    /** Método LoadStates
     * Carga los estados que tiene de componentes
     * CArga los estados que pasamos por parametros
     * @param params BAsicStates[] state Listado de estados agregados por comas */
    public void LoadStates() {
        LoadStates(GetComponents<BasicState>());
    }
    public void LoadStates(params BasicState[] state) {
        foreach (BasicState s in state) {
            if (Security >= fsmSecurity.Hard) {
                if (s.gameObject != gameObject) {
                    Debug.LogWarning("StateMachine can't hold another gameObject state.");
                    return;
                }
            }
            ILoadStates(s);
        }
    }

    /** Método ILoadStates
     * Método interno que carga los estados en la maquina
     * @param BasicState state Estado para cargar */
    private void ILoadStates(BasicState state) {
        if (States == null) {
            States = new List<BasicState>();
        }
        if (States.Contains(state)) {
            return;
        }
        States.Add(state);
        state.StateMachine = this;
        state.CreateTransitions();
    }

    /** Método ChangeState
     * Cmabia de estados entre el current y el state
     * Tiene en cuenta la seguridad de la máquina para agregar o no el estado
     * @param BasicState state Estado destino
     * @param Action trigger Callback del trigger del StateTransition */
    public void ChangeState(BasicState state, Action trigger = null) {
        if (!States.Contains(state)) {
            if (Security >= fsmSecurity.Soft) {
                LoadStates(state);
                Debug.LogWarning("StateMachine load " + state.Name + " satate on the flow.");
            } else if (Security >= fsmSecurity.Hard) {
                Debug.LogWarning("StateMachine can change to " + state.Name + ".");
                return;
            }
        }
        CurrentState.OnExit();
        if (trigger != null)
            trigger();
        CurrentState = state;
        CurrentState.OnEnter();
    }

    /** Método StartMachine 
     * @param SimpleState Estado inicla o (null, primer estado de la máquina) */
    public void StartMachine() {
        if (Security >= fsmSecurity.Hard) {
            if (InnitialState == null) {
                Debug.LogWarning("StateMachine cant' start with empty InnitialState.");
                return;
            }
        }
        CurrentState = InnitialState;
        CurrentState.OnEnter();
    }

    /** Método UpdateMachine 
     * Actualiza la maquina, hacinedo el OnState de los estados
     * Comprueba las transiciones y ejecuta todo el tinglao */
    public void UpdateMachine() {
        if (Status.Equals(state.Inactive)) {
            return;
        }
        foreach (StateTransition t in CurrentState.Transitions) {
            if (t.CheckTransition()) {
                ChangeState(t.Next(), t.OnTrigger);
                return;
            }
        }
        if (CurrentState.Status.Equals(state.Active)) {
            CurrentState.ActiveTime();
            CurrentState.OnState();
        } else {
            CurrentState.InactiveTime();
        }
    }

}

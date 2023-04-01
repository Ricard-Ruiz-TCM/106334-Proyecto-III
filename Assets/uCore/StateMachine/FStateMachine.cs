using System;
using System.Collections.Generic;
using UnityEngine;

/** class StateMachine
 * -----------------
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v4.0 (02/2023)
 */

public class FStateMachine : MonoBehaviour {

    /** State Core */
    [Header("Status & Security:")]
    public status Status;
    public fsmSecurity Security;

    /** Relevant BasicStates */
    [HideInInspector]
    public BasicState InnitialState;
    [SerializeField, Header("Current State:")]
    private BasicState CurrentState;

    /** All BasicStates */
    [SerializeField, Header("All Machine States:")]
    private List<BasicState> States;

    // Load States 
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

    private void ILoadStates(BasicState state) {
        if (States == null) {
            States = new List<BasicState>(); }
        if (States.Contains(state)) {
            return; }
        States.Add(state);
        state.StateMachine = this;
        state.CreateTransitions();
    }

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
        if (trigger != null) trigger();
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

    public void UpdateMachine() {
        if (Status.Equals(status.Inactive)) {
            return; }
        foreach (StateTransition t in CurrentState.Transitions) {
            if (t.CheckTransition()) {
                ChangeState(t.Next(), t.OnTrigger);
                break;
            }
        }
        if (CurrentState.Status.Equals(status.Active)) {
            CurrentState.ActiveTime();
            CurrentState.OnState();
        } else {
            CurrentState.InactiveTime();
        }
    }

}
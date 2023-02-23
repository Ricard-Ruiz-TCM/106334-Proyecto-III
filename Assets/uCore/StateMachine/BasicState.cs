using System.Collections.Generic;
using UnityEngine;

/** abstract class BasicState
 * --------------------------
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v4.0 (02/2023)
 * */

public abstract class BasicState : MonoBehaviour, IState {

    /** Core */
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public status Status;

    /** Timer control */
    protected float TimeActive;
    protected float TimeInactive;

    /** State Machine */
    [HideInInspector]
    public FStateMachine StateMachine;
    public List<StateTransition> Transitions;

    /** Método abstract para crear transiciones de este estado concreto */
    public abstract void CreateTransitions();

    /** Métodos abstract de IState */
    public abstract void OnEnter();
    public abstract void OnState();
    public abstract void OnExit();

    /** Time Control */
    public void ActiveTime() { TimeActive += Time.deltaTime; } 
    public void InactiveTime() { TimeInactive += Time.deltaTime; }
    public void ResetTime() { TimeActive = 0.0f; TimeInactive = 0.0f; }

    /** Transition control */
    public void AddTransition(StateTransition transition, BasicState next) {
        if (Transitions == null) Transitions = new List<StateTransition>();
        Transitions.Add(transition.Destiny(next));
    }

}

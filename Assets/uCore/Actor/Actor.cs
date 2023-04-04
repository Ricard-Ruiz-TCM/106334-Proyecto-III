using UnityEngine;

/** abstract class uCActor
 * --------------------------
 * 
 * Estructura para gestionar los Actores de un juego
 * Pensada para ser el "contenedor" de la StateMachine
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v3.0 (12/2022)
 * 
 */

[RequireComponent(typeof(FStateMachine))]
public abstract class Actor : MonoBehaviour {

    /** StateMachine */
    protected FStateMachine StateMachine;
    protected abstract void ConstructMachine();

    protected void Awake() {
        StateMachine = GetComponent<FStateMachine>();
        ConstructMachine();
    }

    protected void Start() {
        StateMachine.StartMachine();
    }

    protected void Update() {
        StateMachine.UpdateMachine();
    }

    protected void CreateTransitions(BasicState from, BasicState to, StateTransition.TConditionDel condition, StateTransition.TTriggerDel trigger = null) {
        from.AddTransition(new StateTransition(condition, trigger), to);
    }

    protected void AddTransition(BasicState from, StateTransition transition, BasicState to) {
        from.AddTransition(transition, to);
    }

}


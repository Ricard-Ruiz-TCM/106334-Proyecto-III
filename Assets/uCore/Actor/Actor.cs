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

    public abstract void OnStart();
    public abstract void OnAwake();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();

    private void Awake() {
        StateMachine = GetComponent<FStateMachine>();
        ConstructMachine();
        OnAwake();
    }

    private void Start() {
        StateMachine.StartMachine();
        OnStart();
    }

    private void Update() {
        StateMachine.UpdateMachine();
        OnUpdate();
    }

    private void FixedUpdate() {
        OnFixedUpdate();
    }

}


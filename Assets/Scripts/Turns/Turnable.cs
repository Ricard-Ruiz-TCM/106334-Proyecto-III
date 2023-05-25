using UnityEngine;

public abstract class Turnable : MonoBehaviour {

    // Unity Start
    protected virtual void Start() {
        TurnManager.instance.subscribe(this);
    }

    // Unity OnDestroy
    protected virtual void OnDestroy() {
        TurnManager.instance.unsubscribe(this);
    }

    /** Métodos de control del turno **/
    public turnState state;
    public abstract void thinking();
    public virtual void endTurn() {
        moving = progress.done;
        acting = progress.done;
        state = turnState.completed;
    }
    public virtual void beginTurn() {
        moving = progress.ready;
        acting = progress.ready;
        state = turnState.thinking;
    }
    public virtual bool isTurnDone() {
        if (isActingDone() && isMovementDone()) {
            state = turnState.completed;
        }
        return state.Equals(turnState.completed);
    }
    /** ---------------------------- **/

    /** Métodos de control de Acción */
    public progress acting;
    public abstract void act();
    public virtual void reAct() {
        acting = progress.ready;
        state = turnState.thinking;
    }
    public virtual bool canAct() {
        return acting.Equals(progress.ready);
    }
    public virtual void startAct() 
    {
        state = turnState.acting;
        acting = progress.doing;
    }
    public virtual void endAction() {
        acting = progress.done;
        state = turnState.thinking;
    }
    public virtual bool isActingDone() {
        return acting.Equals(progress.done);
    }
    /** ---------------------------- */

    /** Métodos de control de Movimiento */
    public progress moving;
    public abstract void move();
    public virtual bool canMove() {
        return moving.Equals(progress.ready);
    }
    public virtual void startMove() 
    {
        state = turnState.moving;
        moving = progress.doing;
    }
    public virtual void endMovement() {
        moving = progress.done;
        state = turnState.thinking;
    }
    public virtual bool isMovementDone() {
        return moving.Equals(progress.done);
    }
    /** -------------------------------- */


}
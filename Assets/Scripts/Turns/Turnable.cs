using UnityEngine;

public abstract class Turnable : MonoBehaviour {

    // Unity Start
    protected virtual void Start() {
        TurnManager.instance.subscribe(this);
    }

    /** Métodos de control del turno **/
    public turnState state;
    public abstract void onTurn();
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
    public virtual bool canAct() {
        return !isActingDone();
    }
    public virtual void allowAct() {
        if (canAct())
            state = turnState.acting;
    }
    public virtual void startAct() {
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
        return !isMovementDone();
    }
    public virtual void allowMovement() {
        if (canMove())
            state = turnState.moving;
    }
    public virtual void startMove() {
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
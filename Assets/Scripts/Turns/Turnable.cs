public abstract class Turnable : ActorManager {

    /** Métodos de control del turno **/
    public turnState state;
    public turnState State() {
        return state;
    }

    public virtual void EndTurn() {
        state = turnState.completed;
    }
    public virtual void BeginTurn() {
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
    public abstract void Act();
    public virtual bool CanAct() {
        return state.Equals(turnState.acting);
    }
    public virtual void AllowAct() {
        state = turnState.acting;
    }
    
    public virtual void StartAct() {
        acting = progress.doing;
    }
    public virtual void EndAction() {
        acting = progress.done;
    }
    public virtual bool isActingDone() {
        return moving.Equals(progress.done);
    }
    /** ---------------------------- */

    /** Métodos de control de Movimiento */
    public progress moving;
    public abstract void Move();
    public virtual bool CanMove() {
        return state.Equals(turnState.moving);
    }
    public virtual void AllowMovement() {
        state = turnState.moving;
    }

    public virtual void StartMove() {
        moving = progress.doing;
    }
    public virtual void EndMovement() {
        moving = progress.done;
    }
    public virtual bool isMovementDone() {
        return moving.Equals(progress.done);
    }
    /** -------------------------------- */

}
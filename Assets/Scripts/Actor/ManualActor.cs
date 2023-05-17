using UnityEngine;

public class ManualActor : Actor {

    /** Override del onTurn */
    public override void onTurn() {
        if (uCore.Action.GetKeyDown(KeyCode.M)) {
            setDestination(Stage.Pathfinder.FindPath(Stage.StageBuilder.GetGridPlane(transform.position).node, Stage.StageBuilder.GetMouseGridPlane().node));
        }

        if (uCore.Action.GetKeyDown(KeyCode.J)) {
            allowAct();
        }
    }

    /** Override del beginTurn */
    public override void beginTurn() {
        base.beginTurn();
    }

    /** Override del endTurn */
    public override void endTurn() {
        base.endTurn();
    }

    /** Override del canAct */
    public override bool canAct() {
        return base.canAct();
    }

    /** Override del act */
    public override void act() {
        endAction();
    }

    /** Override CanMove from Turnable */
    public override bool canMove() {
        return base.canMove();
    }

    /** Override Move from Turnable */
    public override void move() {
        base.move();
    }

    public override void onActorDeath() {

    }
}

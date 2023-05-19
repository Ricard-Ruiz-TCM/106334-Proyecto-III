using UnityEngine;
using System.Collections.Generic;

public class ManualActor : Actor {

    /** Override del onTurn */
    public override void thinking() {

        if (uCore.Action.GetKeyDown(KeyCode.M)) {
            setDestination(Stage.Pathfinder.FindPath(Stage.StageBuilder.getGridNode(transform.position), Stage.StageBuilder.getMouseGridPlane().node));
            startMove();
        }

        if (uCore.Action.GetKeyDown(KeyCode.J)) {
            startAct();
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

}

using UnityEngine;

public class AutomaticActor : Actor {

    /** Override del onTurn */
    public override void onTurn() {
        
        if (canAct()) {
            allowAct();
        }

        if (!isMovementDone()) {
            setDestination(Stage.Pathfinder.FindPath(Stage.StageBuilder.GetGridPlane(transform.position).node, Stage.StageBuilder.GetGridPlane(Random.Range(1, 13), Random.Range(1, 13)).node));
        }

    }

    /** Override del act */
    public override void act() {
        endAction();
    }

    public override void onActorDeath() {
        
    }
}

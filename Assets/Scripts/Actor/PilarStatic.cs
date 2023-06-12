using System.Collections.Generic;
using UnityEngine;

public class PilarStatic : StaticActor {

    [SerializeField] private int pilarHeight = 0;
    [SerializeField] private GameObject pilar;
    [SerializeField] private int damageFromPilar = 1;

    private BasicActor damageFrom;
    private Node pilarNode;

    protected override void Start() {
        base.Start();
        pilarNode = Stage.StageBuilder.getGridNode(this.transform.position);
    }

    public override void onActorDeath() {

        Vector2 direction = Stage.StageBuilder.getDirection(this.transform.position, damageFrom.transform.position);
        direction = direction * pilarHeight;
        Node endPoint = Stage.StageBuilder.getGridNode(pilarNode.x + (int)direction.x, pilarNode.y + (int)direction.y);

        GameObject objToSpawn = new GameObject("Cool GameObject made from Code");
        objToSpawn.transform.position = Stage.StageBuilder.getGridPlane(endPoint).transform.position;
        theCollumnGoesDown(objToSpawn);

        List<Node> nodesToSetNotWalkable = Stage.Pathfinder.FindPath(pilarNode, endPoint, false);

        Stage.Grid.changeNodeType(endPoint.x, endPoint.y, Array2DEditor.nodeType.X);

        BasicActor endPointActor = Stage.StageManager.getActor(endPoint);
        if (endPointActor != null)
            endPointActor.takeDamage(this, damageFromPilar, itemID.NONE);

        foreach (var node in nodesToSetNotWalkable) {
            BasicActor nodeActor = Stage.StageManager.getActor(node);
            if (nodeActor != null)
                nodeActor.takeDamage(this, damageFromPilar, itemID.NONE);

            Stage.Grid.changeNodeType(node.x, node.y, Array2DEditor.nodeType.X);
        }

        Stage.StageBuilder.clearGrid();
        TurnManager.instance.unsubscribe(this);
        this.enabled = false;
    }

    public override void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {
        damageFrom = from;
        base.takeDamage(from, damage, weapon);
    }

    private void theCollumnGoesDown(GameObject lookAt) {
        pilar.transform.LookAt(lookAt.transform);
        pilar.transform.Rotate(0, 90, 90);

        Destroy(lookAt);
    }
}

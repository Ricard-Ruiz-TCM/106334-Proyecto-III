using System.Collections.Generic;
using UnityEngine;

public class PilarStatic : StaticActor {

    [SerializeField] private int pilarHeight = 0;
    [SerializeField] private GameObject pilar;

    private BasicActor damageFrom;
    private Node pilarNode;
    private Animator pilarAnimator;

    protected override void Start()
    {        
        base.Start();
        pilarNode = Stage.StageBuilder.getGridNode(this.transform.position);
        pilarAnimator = this.GetComponent<Animator>();
    }

    public override void onActorDeath() {
        
        Vector2 direction = Stage.StageBuilder.getDirection(this.transform.position, damageFrom.transform.position);
        direction = direction * pilarHeight;
        Node endPoint = Stage.StageBuilder.getGridNode(pilarNode.x + (int)direction.x, pilarNode.y + (int)direction.y);
        Debug.Log("ROTATION OF PILAR: " + transform.rotation.y);
        pilarAnimator.SetTrigger("GoDown");

        pilar.transform.rotation.SetLookRotation(Stage.StageBuilder.getGridPlane(endPoint).transform.position);
        //transform.LookAt(new Vector3(endPoint.x, endPoint.y, transform.position.z));

        //transform.rotation.SetLookRotation(Vector3.zero);

        Debug.Log("ROTATION OF PILAR: " + transform.rotation.y);

        

        List<Node> nodesToSetNotWalkable = Stage.Pathfinder.FindPath(pilarNode, endPoint, false);

        Stage.Grid.changeNodeType(endPoint.x, endPoint.y, Array2DEditor.nodeType.X);

        foreach (var node in nodesToSetNotWalkable)
        {
            Stage.Grid.changeNodeType(node.x, node.y, Array2DEditor.nodeType.X);
        }

        Stage.StageBuilder.clearGrid();
        TurnManager.instance.unsubscribe(this);
        this.enabled = false;
    }

    /*private void lookAtObjective(Node endPoint) 
    {
        Quaternion targetRotation;

        Vector3 targetPoint = new Vector3(endPoint.x, endPoint.y, transform.position.z) - transform.position;

        targetRotation = Quaternion.LookRotation(-targetPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
    }*/

    public override void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {
        damageFrom = from;
        base.takeDamage(from, damage, weapon);
    }
}

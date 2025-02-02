﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PilarStatic : StaticActor {

    [SerializeField] private int pilarHeight = 0;
    [SerializeField] private GameObject pilar;
    [SerializeField] private int damageFromPilar = 1;

    private BasicActor damageFrom;
    private Node pilarNode;

    [SerializeField] float duration;

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
        theCollumnGoesDown(objToSpawn,endPoint);

        
    }

    public override void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {
        damageFrom = from;
        base.takeDamage(from, damage, weapon);
    }

    private void theCollumnGoesDown(GameObject lookAt,Node endPoint) {
        pilar.transform.LookAt(lookAt.transform);
        StartCoroutine(GoDownColumn(endPoint));
        Destroy(lookAt);
    }
    IEnumerator GoDownColumn(Node endPoint)
    {
        float timer = 0;
        Vector3 initialRot = pilar.transform.eulerAngles;
        Vector3 finalRot = new Vector3(pilar.transform.eulerAngles.x, pilar.transform.eulerAngles.y + 90, pilar.transform.eulerAngles.z + 90);
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float percentageDuration = timer / duration;
            pilar.transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRot, finalRot, percentageDuration));
            yield return null;
        }
        pilar.transform.eulerAngles = finalRot;

        List<Node> nodesToSetNotWalkable = Stage.Pathfinder.FindPath(pilarNode, endPoint, false);

        Stage.Grid.changeNodeType(endPoint.x, endPoint.y, Array2DEditor.nodeType.C);

        BasicActor endPointActor = Stage.StageManager.getActor(endPoint);
        if (endPointActor != null && endPointActor != this)
            endPointActor.takeDamage(this, damageFromPilar);

        foreach (var node in nodesToSetNotWalkable)
        {
            BasicActor nodeActor = Stage.StageManager.getActor(node);
            if (nodeActor != null && nodeActor != this)
                nodeActor.takeDamage(this, damageFromPilar);

            Stage.Grid.changeNodeType(node.x, node.y, Array2DEditor.nodeType.C);
        }

        Stage.Grid.changeNodeType(pilarNode.x, pilarNode.y, Array2DEditor.nodeType.C);

        Stage.StageBuilder.clearGrid();
        TurnManager.instance.unsubscribe(this);
        this.enabled = false;

    }
}

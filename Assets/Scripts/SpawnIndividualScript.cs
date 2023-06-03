using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndividualScript : MonoBehaviour
{
    [SerializeField] List<GameObject> spawns;
    // Update is called once per frame
    void Update()
    {
        if (Stage.StageBuilder._grid != null)
        {
            Node myNode = Stage.StageBuilder.getGridNode(transform.position);
            Stage.Grid.changeNodeType(myNode.x, myNode.y, Array2DEditor.nodeType.P);
            Stage.StageBuilder.clearGrid();
            Destroy(gameObject);
        }
    }
}

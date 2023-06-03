using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelimitationScript : MonoBehaviour
{
    [SerializeField] List<GameObject> delimitations;
    // Update is called once per frame
    void Update()
    {
        if (Stage.StageBuilder._grid != null)
        {
            foreach(var delimitation in delimitations)
            {
                Node myNode = Stage.StageBuilder.getGridNode(delimitation.transform.position);
                Stage.Grid.changeNodeType(myNode.x, myNode.y, Array2DEditor.nodeType.X);
                Stage.StageBuilder.clearGrid();                
            }
            Destroy(this.gameObject);
        }
    }
}

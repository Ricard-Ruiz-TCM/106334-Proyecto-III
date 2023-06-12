using System.Collections.Generic;
using UnityEngine;

public class DelimitationIndividualScript : MonoBehaviour {
    [SerializeField] List<GameObject> delimitations;
    // Update is called once per frame
    void Update() {
        if (Stage.StageBuilder._grid != null) {
            Node myNode = Stage.StageBuilder.getGridNode(transform.position);
            Stage.Grid.changeNodeType(myNode.x, myNode.y, Array2DEditor.nodeType.X);
            Stage.StageBuilder.clearGrid();
            Destroy(gameObject);
        }
    }
}

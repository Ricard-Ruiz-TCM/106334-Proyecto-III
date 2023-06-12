using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    [SerializeField] List<GameObject> spawns;
    // Update is called once per frame
    void Update() {
        if (Stage.StageBuilder._grid != null) {
            foreach (var spawn in spawns) {
                Node myNode = Stage.StageBuilder.getGridNode(spawn.transform.position);
                Stage.Grid.changeNodeType(myNode.x, myNode.y, Array2DEditor.nodeType.P);
                Stage.StageBuilder.clearGrid();
            }
            Destroy(this.gameObject);
        }
    }
}

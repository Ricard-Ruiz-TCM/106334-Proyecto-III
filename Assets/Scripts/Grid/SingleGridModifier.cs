using UnityEngine;
using Array2DEditor;

public class SingleGridModifier  : MonoBehaviour{

    [SerializeField]
    private nodeType _newType;

    // Unity OnEnable
    void OnEnable() {
        GridBuilder.onGridBuild += changeNode;
    }

    // Unity OnDisable
    void OnDisable() {
        GridBuilder.onGridBuild -= changeNode;
    }

    /** Método para cambair el nodo de tipo al asignado */
    private void changeNode() {
        Stage.Grid.changeNodeType(transform.position, _newType);
    }

}

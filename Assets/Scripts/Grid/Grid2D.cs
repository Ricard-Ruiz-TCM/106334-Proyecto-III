using Array2DEditor;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour {

    // Filas & Columnas de Nuestro Grid
    public int rows {
        get {
            return _gridMap.GridSize.x;
        }
    }
    public int columns {
        get {
            return _gridMap.GridSize.y;
        }
    }

    // Mapa de nodos
    private Node[,] _nodeMap;
    public Node[,] nodeMap => _nodeMap;

    [SerializeField, Header("(x Right, z Down)")]
    private Array2DNodeEnum _gridMap;

    // Unity Awake
    void Awake() {
        _nodeMap = new Node[rows, columns];
        for (int x = 0; x < rows; x++) {
            for (int y = 0; y < columns; y++) {
                _nodeMap[x, y] = new Node(_gridMap.GetCell(x, y), x, y);
            }
        }
    }

    // Unity Start
    void Start() {
        Stage.StageBuilder.instantiateGrid();
    }

    /** Get del NodeMap */
    public Node[,] getNodeMap() {
        return _nodeMap;
    }

    /** Get de Nodo por coord */
    public Node getNode(int x, int y) {
        if (insideGrid(x, y))
            return _nodeMap[x, y];
        return null;
    }

    /** Get de los nodos adyacientes al Nodo node */
    public List<Node> getNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        if (insideGrid(node.x + 1, node.y))
            neighbours.Add(_nodeMap[node.x + 1, node.y]);

        if (insideGrid(node.x - 1, node.y))
            neighbours.Add(_nodeMap[node.x - 1, node.y]);

        if (insideGrid(node.x, node.y + 1))
            neighbours.Add(_nodeMap[node.x, node.y + 1]);

        if (insideGrid(node.x, node.y - 1))
            neighbours.Add(_nodeMap[node.x, node.y - 1]);

        return neighbours;
    }

    /** Comprueba si la cordenada está dentro del Grid*/
    public bool insideGrid(int x, int y) {
        return ((x >= 0) && (x < rows) && (y >= 0) && (y < columns));
    }

    /** Métood que comprueba si un nodo es de cierto typo */
    public bool isType(int x, int y, nodeType type) {
        return getNode(x, y).type.Equals(type);
    }

}
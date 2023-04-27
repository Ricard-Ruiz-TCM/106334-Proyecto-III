using Array2DEditor;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{

    // Filas & Columnas de Nuestro Grid
    public int Rows
    {
        get
        {
            return _gridMap.GridSize.x;
        }
    }
    public int Columns
    {
        get
        {
            return _gridMap.GridSize.y;
        }
    }

    // Mapa de nodos
    private Node[,] _nodeMap;

    [SerializeField, Header("(z Right, x Down)")]
    private Array2DBool _gridMap;

    // Unity Awake
    void Awake()
    {
        _nodeMap = new Node[Rows, Columns];
        for (int x = 0; x < Rows; x++)
        {
            for (int y = 0; y < Columns; y++)
            {
                _nodeMap[x, y] = new Node(!_gridMap.GetCell(x, y), x, y);
            }
        }
    }

    /** Get del NodeMap */
    public Node[,] GetNodeMap()
    {
        return _nodeMap;
    }
    /** Get de Nodo por coord */
    public Node GetNode(int x, int y)
    {
        if (insideGrid(x, y))
            return _nodeMap[x, y];
        return null;
    }
    /** Get de los nodos adyacientes al Nodo node */
    public List<Node> GetNeighbours(Node node)
    {
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
    public bool insideGrid(int x, int y)
    {
        return ((x >= 0) && (x < Rows) && (y >= 0) && (y < Columns));
    }

}
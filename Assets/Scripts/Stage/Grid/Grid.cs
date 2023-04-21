using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inspector2dArray))]
public class Grid : MonoBehaviour
{

    public int rows = 10;
    public int columns = 10;
    [SerializeField]
    private Node[,] grid;
    private Inspector2dArray array;

    void Awake()
    {
        array = GetComponent<Inspector2dArray>();
        CreateGrid(rows, columns);
    }

    public void CreateGrid(int row, int column)
    {
        grid = new Node[row, column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                grid[i, j] = new Node(!array.columns[j].rows[i], i, j);
            }
        }

    }

    public Node[,] GridMap()
    {
        return grid;
    }

    public Node GetNode(int x, int y)
    {
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        int checkX = node.gridX + 1;
        int checkY = node.gridY;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }


        checkX = node.gridX - 1;
        checkY = node.gridY;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }

        checkX = node.gridX;
        checkY = node.gridY + 1;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }
        checkX = node.gridX;
        checkY = node.gridY - 1;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }



        return neighbours;
    }

}
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class Pathfinding : MonoBehaviour
{
    Grid _grid;

    void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    public List<Node> FindPath(Node startNode, Node targetNode)
    {

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
               return RetracePath(startNode, targetNode);

            }

            foreach (Node neighbour in _grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return null;
    }


    List<Node> RetracePath(Node startNode, Node endNode)
    {

        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        //int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        //int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        //if (dstX > dstY)
        //    return 14 * dstY + 10 * (dstX - dstY);
        //return 14 * dstX + 10 * (dstY - dstX);

        return Mathf.Abs(nodeA.gridX - nodeB.gridX) + Mathf.Abs(nodeA.gridY - nodeB.gridY);
    }
}

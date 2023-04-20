using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPos;
    public GameObject gridObj;

    public int gCost;
    public int hCost;

    public int gridX;
    public int gridY;

    public Node parent;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node(bool _walkable, Vector3 _worldPos,GameObject obj, int x,int y)
    {
        walkable = _walkable;
        worldPos = _worldPos;
        gridObj = obj;
        gridX = x;
        gridY = y;
    }

}

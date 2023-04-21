using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;

    public int gridX;
    public int gridY;

    public Node parent;

    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node(bool _walkable, int x,int y)
    {
        walkable = _walkable;
        gridX = x;
        gridY = y;
    }

}

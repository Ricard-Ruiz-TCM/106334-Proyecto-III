using System;
using Array2DEditor;

[Serializable]
public class Node {

    // Cords
    public int x, y;
    // Heuristic Costs
    public int g, h;
    public int f {
        get {
            return g + h;
        }
    }

    // Check if node is walkable
    public bool walkable {
        get {
            return !type.Equals(nodeType.X);
        }
    }

    public nodeType type;
    public Node parent;

    // Constructor
    public Node(nodeType nType, int xPos, int yPos) {
        x = xPos;
        y = yPos;
        type = nType;
    }

}

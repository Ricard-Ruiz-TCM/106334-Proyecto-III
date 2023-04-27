
public class Node
{

    // Cords
    public int x, y;
    // Heuristic Costs
    public int g, h;
    public int f
    {
        get { return g + h; }
    }

    // ValidPosition
    public bool walkable;
    public Node parent;

    // Constructor
    public Node(bool isWalkable, int xPos, int yPos)
    {
        walkable = isWalkable; x = xPos; y = yPos;
    }

}

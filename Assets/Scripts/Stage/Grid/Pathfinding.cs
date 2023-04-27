using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid2D))]
public class Pathfinding : MonoBehaviour {

    public enum heuristic {
        Manhattan, Diagonal, Euclidean
    }

    // Grid
    private Grid2D _grid;

    [SerializeField]
    private heuristic _heuristic;

    // Unity Awake
    void Awake() {
        _grid = GetComponent<Grid2D>();
    }

    /** Método FindPath, busca la mejor ruta entre 2 nodos, devuelve la lista de origin -> target **/
    public List<Node> FindPath(Node origin, Node target) {

        // Listas
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(origin);

        while (openList.Count > 0) {
            Node node = openList[0];

            // Buscamos el nodo con coste en h más bajo
            for (int i = 1; i < openList.Count; i++) {
                if (openList[i].f < node.f || openList[i].f == node.f) {
                    if (openList[i].h < node.h)
                        node = openList[i];
                }
            }

            openList.Remove(node);
            closedList.Add(node);

            // Llegamos al destino
            if (node == target) {
                return ReversePath(origin, target);
            }

            // Revisamos los vecinos al nodo en cuestión 
            foreach (Node neighbour in _grid.GetNeighbours(node)) {
                if (!neighbour.walkable || closedList.Contains(neighbour)) {
                    continue;
                }
                // Calculamos nuevo coste al neighbour teniendo origen en cuenta
                int newCostToNeighbour = node.g + Heuristic(node, neighbour);
                if (newCostToNeighbour < neighbour.g || !openList.Contains(neighbour)) {
                    neighbour.g = newCostToNeighbour;
                    neighbour.h = Heuristic(neighbour, target);
                    neighbour.parent = node;
                    // Añadimos el neigbour a la lista
                    if (!openList.Contains(neighbour)) {
                        openList.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }


    /** Metodo para tornar el path al rever, utilizando el parent del Node */
    private List<Node> ReversePath(Node origin, Node target) {
        List<Node> path = new List<Node>();
        Node currentNode = target;
        while (currentNode != origin) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }


    /** Método para calcualr la heuristica */
    private int Heuristic(Node origin, Node target) {
        switch (_heuristic) {
            case heuristic.Manhattan:
                return Mathf.Abs(origin.x - target.x) + Mathf.Abs(origin.y - target.y);

            case heuristic.Diagonal:
                int x = Mathf.Abs(origin.x - target.x);
                int y = Mathf.Abs(origin.y - target.y);
                if (x > y)
                    return 14 * y + 10 * (x - y);
                return 14 * x + 10 * (y - x);

            case heuristic.Euclidean:
                return (int)Mathf.Sqrt((origin.x - target.x) * 2 + (origin.y - target.y) * 2);
        }
        return 0;
    }
}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid2D))]
public class GridBuilder : MonoBehaviour {

    [HideInInspector]
    public Grid2D _grid;
    private GridPlane[,] _planeMap;

    [SerializeField, Header("Plane Prefab for Grid:")]
    private GameObject _planePfb;
    [SerializeField, Tooltip("gpl = GridPanelSize => Lo que mide el mesh del panel, pues scale 1 => 10")]
    private float _planeSize;
    [SerializeField]
    private float _offset;

    [SerializeField, Header("Terrain Layer:")]
    private LayerMask _layer;

    [SerializeField, Header("Materials:"), Tooltip("Tiene que coincidir con el enum pathMaterial")]
    private Material[] _materials;

    [SerializeField, Header("Layers:")]
    private string _visibleLayer = "VisibleGrid";
    [SerializeField]
    private string _invisibleLayer = "InvisibleGrid";

    // Unity Start
    void Start() {
        // Clear al acabar turnos
        Actor.onDestinationReached += clearGrid;
    }

    /** Método para instanciar los planos */
    public void instantiateGrid() {
        _grid = GetComponent<Grid2D>();
        _planeMap = new GridPlane[_grid.rows, _grid.columns];
        // Instanciación de los paneles
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                // Posición donde será isntanciado
                Vector3 position = new Vector3(x * _planeSize * _planePfb.transform.localScale.x + _offset, _offset, y * _planeSize * _planePfb.transform.localScale.y + _offset);
                // Instant del prefab
                GridPlane obj = GameObject.Instantiate(_planePfb, position, Quaternion.identity, transform).GetComponent<GridPlane>();
                obj.gameObject.name = "M[" + x + "," + y + "]-" + "W:" + _grid.getNode(x, y).walkable;
                RaycastHit raycastHit;
                if (Physics.Raycast(obj.transform.position, -Vector3.up, out raycastHit, Mathf.Infinity, _layer)) {
                    // RePosition del plane, justo encima del mapeado
                    obj.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.1f, raycastHit.point.z);
                }
                // initialization
                obj.setGrid(_grid, _grid.getNode(x, y));
                // Assign en el planeMap
                _planeMap[x, y] = obj;
                // Set del material inical
                hideNode(x, y);
            }
        }
    }

    /** Método que compruba si el ratón está sobre la Grid2D */
    public bool isMouseOverGrid() {
        return getMouseGridPlane() != null;
    }

    /** Getters de Node */
    public Node getGridNode(int x, int y) {
        return getGridPlane(x, y).node;
    }
    public Node getGridNode(Vector3 worldPos) {
        return getGridPlane(worldPos).node;
    }
    public Node getMouseGridNode() {
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(uCore.Action.mousePosition), out raycastHit)) {
            return getGridPlane(raycastHit.point).node;
        }
        return null;
    }

    /** Getters de GridPlane */
    public GridPlane getGridPlane(Node node) {
        return getGridPlane(node.x, node.y);
    }
    public GridPlane getGridPlane(int x, int y) {
        return _planeMap[x, y];
    }
    public GridPlane getGridPlane(Vector3 worldPos) {
        return getGridPlane(Mathf.RoundToInt(worldPos.x - _offset), Mathf.RoundToInt(worldPos.z - _offset));
    }
    public GridPlane getMouseGridPlane() {
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(uCore.Action.mousePosition), out raycastHit)) {
            return getGridPlane(raycastHit.point);
        }
        return null;
    }
    
    /** Getters de Distance */
    public int getDistance(Vector3 a, Vector3 b) {
        return getDistance(getGridPlane(a), getGridPlane(b));
    }
    public int getDistance(GridPlane a, GridPlane b) {
        return getDistance(a.node, b.node);
    }
    public int getDistance(Node a, Node b) {
        return calcDistance(a.x, b.x, a.y, b.y);
    }

    /** Método para calcualr la distancia */
    private int calcDistance(int x1, int x2, int y1, int y2) {
        return Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2);
    }

    /** Método para calcular la dirección entre 2 */
    public Vector2 getDirection(Vector3 a, Vector3 b) {
        return getDirection(getGridPlane(a), getGridPlane(b));
    }
    public Vector2 getDirection(GridPlane a, GridPlane b) {
        return getDirection(a.node, b.node);
    }
    public Vector2 getDirection(Node a, Node b) {
        return calcDirection(a.x, b.x, a.y, b.y);
    }

    /** Método para calcular la dirección */
    private Vector2 calcDirection(int x1, int x2, int y1, int y2) {
        return new Vector2(Mathf.Clamp((x1 - x2), -1, 1), Mathf.Clamp((y1 - y2), -1, 1));
    }

    /** Método para localizar el GridPlane más cercano a un objetivo */
    public GridPlane findClosestGrid(GridPlane origin, GridPlane target) {
        int tmp = 1000;
        Node node = null;
        List<Node> neighbours = _grid.getNeighbours(target.node);
        foreach (Node cell in neighbours) {
            if (cell != target.node) {
                int dis = getDistance(origin.node, cell);
                if (dis < tmp) {
                    tmp = dis;
                    node = cell;
                }
            }
        }
        return getGridPlane(node);
    }

    /** Método para mostar un path concreto */
    public void displayPath(List<Node> path, pathMaterial material) {
        for (int i = 0; i < path.Count; i++) {
            displayNode(path[i], material);
        }
    }
    
    /** Método para mostar un solo nodo */
    public void displayNode(Node node, pathMaterial material) {
        displayNode(node.x, node.y, material);
    }
    public void displayNode(int x, int y, pathMaterial material) {
        getGridPlane(x, y).setRendering(_materials[(int)material], _visibleLayer);
    }

    /** Método para mostar una skill concreta */
    public void displaySkill(skillID id, Node node, pathMaterial material)
    {
        Skill sk = uCore.GameManager.GetSkill(id);

        displayNode(node, material);

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - sk.areaRange; i <= node.x + sk.areaRange; i++)
        {
            for (int j = node.y; j < count + node.y; j++)
            {
                displayNode(i, j, material);
            }
            for (int z = node.y - 1; z > node.y - count; z--)
            {
                displayNode(i, z, material);
            }

            if (totalCount > sk.areaRange)
            {
                count--;
            }
            else
            {
                count++;
            }
            totalCount++;
        }


    }

    /*** Método para ocultar un nodo */
    public void hideNode(Node node) {
        hideNode(node.x, node.y);
    }
    public void hideNode(int x, int y) {
        getGridPlane(x, y).setLayer(_invisibleLayer);
    }
    public void displaySkillRange(int range, Node node,pathMaterial mat = pathMaterial.skill)
    {

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - range; i <= node.x + range; i++)
        {
            for (int j = node.y; j < count + node.y; j++)
            {
                if (Stage.Grid.insideGrid(i, j))
                {
                    getGridPlane(i, j).setMaterial(_materials[(int)mat]);
                }
            }
            for (int z = node.y - 1; z > node.y - count; z--)
            {
                if (Stage.Grid.insideGrid(i, z))
                {
                    getGridPlane(i, z).setMaterial(_materials[(int)mat]);
                }
            }

            if (totalCount > range)
            {
                count--;
            }
            else
            {
                count++;
            }
            totalCount++;
        }
    }

    /** Método para limpiar el grid y hacerlo invisible */
    public void clearGrid() {
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                hideNode(x, y);
            }
        }
    }

}

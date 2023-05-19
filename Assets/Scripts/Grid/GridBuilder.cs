using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid2D))]
public class GridBuilder : MonoBehaviour {

    [HideInInspector]
    public Grid2D _grid;
    private GridPlane[,] _planeMap;

    [SerializeField, Header("Plane Prefab for Grid:")]
    private GameObject _planePfb;
    // gpl = GridPanelSize => Lo que mide el mesh del panel, pues scale 1 => 10
    public float _planeSize = 10f;

    [SerializeField, Header("Terrain Layer:")]
    private LayerMask _layer;

    [SerializeField, Header("Materials_")]
    public Material _pathMath;
    public Material _badPathMat;
    public Material _rangeMath;
    public Material _normalMat;

    // Unity Awake
    void Awake() {
        TurnManager.instance.onEndTurn += ClearGrid;
    }

    /** Método para instanciar los planos */
    public void InstantiatePlanes() {
        _grid = GetComponent<Grid2D>();
        _planeMap = new GridPlane[_grid.Rows, _grid.Columns];
        // Instanciación de los paneles
        for (int x = 0; x < _grid.Rows; x++) {
            for (int y = 0; y < _grid.Columns; y++) {
                // Posición donde será isntanciado
                Vector3 position = new Vector3(x * _planeSize , 50f, y * _planeSize); // he quitado * _planePfb.transform.localScale.x
                // Instant del prefab
                GridPlane obj = GameObject.Instantiate(_planePfb, position, Quaternion.identity, transform).GetComponent<GridPlane>();
                obj.gameObject.name = "M[" + x + "," + y + "]-" + "W:" + _grid.GetNode(x, y).walkable;
                RaycastHit raycastHit;
                if (Physics.Raycast(obj.transform.position, -Vector3.up, out raycastHit, Mathf.Infinity, _layer)) {
                    // RePosition del plane, justo encima del mapeado
                    obj.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.1f, raycastHit.point.z);
                }
                // initialization
                obj.SetGrid(_grid, _grid.GetNode(x, y));
                // Assign en el planeMap
                _planeMap[x, y] = obj;
                // Set del material inical
                UpdateMaterial(x, y);
            }
        }
    }

    // Checka si tenemos el ratón sobre el Grid
    public bool MosueOverGrid() {
        return GetMouseGridPlane() != null;
    }

    /** Métodos para obtener el GridPlane */
    public GridPlane GetGridPlane(Node node) {
        return GetGridPlane(node.x, node.y);
    }
    public GridPlane GetGridPlane(int x, int y) {
        return _planeMap[x, y];
    }
    public GridPlane GetGridPlane(Vector3 worldPos) {
        return GetGridPlane(Mathf.RoundToInt(worldPos.x / _planeSize), Mathf.RoundToInt(worldPos.z / _planeSize));
    }
    public GridPlane GetMouseGridPlane() {
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(uCore.Action.mousePosition), out raycastHit)) {
            if (raycastHit.transform.CompareTag("GridPlane")) {
                return raycastHit.transform.GetComponent<GridPlane>();
            }
        }
        return null;
    }

    public int GetGridDistanceBetween(GridPlane a, GridPlane b) {
        int dx = Mathf.Abs(a.node.x - b.node.x);
        int dy = Mathf.Abs(a.node.y - b.node.y);

        return dx + dy;
    }

    public GridPlane FindClosesGridPlaneTo(GridPlane target, GridPlane origin) {
        int closestDistance = 1000;
        GridPlane closestCell = null;

        List<GridPlane> adsCells = new List<GridPlane>();

        if (_grid.insideGrid(target.node.x, target.node.y + 1))
            adsCells.Add(GetGridPlane(target.node.x, target.node.y + 1));

        if (_grid.insideGrid(target.node.x, target.node.y - 1))
            adsCells.Add(GetGridPlane(target.node.x, target.node.y - 1));

        if (_grid.insideGrid(target.node.x + 1, target.node.y))
            adsCells.Add(GetGridPlane(target.node.x + 1, target.node.y));

        if (_grid.insideGrid(target.node.x - 1, target.node.y))
            adsCells.Add(GetGridPlane(target.node.x - 1, target.node.y));

        foreach (GridPlane cell in adsCells) {
            if (cell != target) {
                int distance = GetGridDistanceBetween(origin, cell);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestCell = cell;
                }
            }
        }

        return closestCell;
    }

    // Update del material del Plane
    public void UpdateMaterial(int x, int y, Material mat = null) 
    {
        GetGridPlane(x, y).gameObject.layer = LayerMask.NameToLayer("VisibleGrid");
        if (mat == null) 
        {
            Node data = GetGridPlane(x, y).node;
            Material m = new Material(Shader.Find("Universal Render Pipeline/Lit"));

            // Color según dificultad
            float c = Mathf.Abs(((float)data.type / 255f) - 1.0f);
            m.SetColor("_BaseColor", new Color(c, c, c));

            // Verde para posicion inicial
            if (data.type.Equals(Array2DEditor.nodeType.P)) {
                m.SetColor("_BaseColor", Color.green);
                GetGridPlane(x, y).gameObject.layer = LayerMask.NameToLayer("VisibleGrid");
            }

            mat = m;
        } else {
            GetGridPlane(x, y).gameObject.layer = LayerMask.NameToLayer("VisibleGrid");
        }

        GetGridPlane(x, y).SetMaterial(mat);
    }

    /** Método para cambiar el path de colores */
    public void DisplayValidPath(List<Node> path, int range) {
        ClearPath();
        for (int i = 0; i < path.Count; i++) {
            if (i < range - 1) 
            {
                GameObject pathObject = GetGridPlane(path[i].x, path[i].y).pathGameObject;
                pathObject.SetActive(true);
                if (path[i + 1].x > path[i].x)
                {
                    pathObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (path[i + 1].x < path[i].x)
                {
                    pathObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (path[i + 1].y > path[i].y)
                {
                    pathObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                if (path[i + 1].y < path[i].y)
                {
                    pathObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                //UpdateMaterial(path[i].x, path[i].y, _pathMath);
            }
        }
    }
    public void DisplayRange(int range, Actor from)
    {
        int count = 1;
        int totalCount = 1;
        Node node = Stage.StageBuilder.GetGridPlane(Mathf.RoundToInt(from.transform.position.x / 10), Mathf.RoundToInt(from.transform.position.z / 10)).node;

        for (int i = node.x - range; i <= node.x + range; i++)
        {
            for (int j = count + node.y-1; j < count + node.y; j++)
            {
                if (CheckIfInGrid(i, j))
                {
                    UpdateMaterial(i, j, _rangeMath);
                    //from.GridM().CalcRoute(from.transform.position, GetGridPlane(i, j), range);
                    //Stage.StageBuilder.DisplayValidPath(from.GridM().VisualRouteValid, range);
                }

            }
            for (int z = node.y - count+1; z > node.y - count; z--)
            {
                if (CheckIfInGrid(i, z))
                {
                    UpdateMaterial(i, z, _rangeMath);
                    //from.GridM().CalcRoute(from.transform.position, GetGridPlane(i, z), range);
                    //Stage.StageBuilder.DisplayValidPath(from.GridM().VisualRouteValid, range);
                }
            }
            if(i == node.x - range || i == node.x + range)
            {
                if (CheckIfInGrid(i, node.y))
                {
                    UpdateMaterial(i, node.y, _rangeMath);
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
    private bool CheckIfInGrid(int x, int y)
    {
        return (x >= 0 && x <= _grid.Rows && y >= 0 && y <= _grid.Columns);
    }
    //public Node DisplayLastNodePath(List<Node> path, int range) {
    //    ClearPath();
    //    if (path.Count != 0) {
    //        if (path.Count < range) {
    //            UpdateMaterial(path[path.Count - 1].x, path[path.Count - 1].y, _rangeMath);
    //            return path[path.Count - 1];
    //        } else {
    //            UpdateMaterial(path[range - 1].x, path[range - 1].y, _rangeMath);
    //            return path[range - 1];
    //        }
    //    } else {
    //        return null;
    //    }

    //}

    public void DisplayInValidPath(List<Node> path) {
        for (int i = 0; i < path.Count; i++) 
        {
            UpdateMaterial(path[i].x, path[i].y, _badPathMat);
        }
    }
    public void DisplaySkillRange(int range,Actor actor)
    {
        Node node = null;
        //List<Node> nodes = new List<Node>();


        node = Stage.StageBuilder.GetGridPlane(Mathf.RoundToInt(actor.transform.position.x / 10), Mathf.RoundToInt(actor.transform.position.z / 10)).node;

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - range; i <= node.x + range; i++)
        {
            for (int j = node.y; j < count + node.y; j++)
            {
                if (CheckIfInGrid(i,j))
                {
                    UpdateMaterial(i, j, _rangeMath);
                }             
            }
            for (int z = node.y - 1; z > node.y - count; z--)
            {
                if (CheckIfInGrid(i, z))
                {
                    UpdateMaterial(i, z, _rangeMath);
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


    public void ClearGrid() 
    {
        for (int x = 0; x < _grid.Rows; x++) {
            for (int y = 0; y < _grid.Columns; y++) {
                UpdateMaterial(x, y);
                //GetGridPlane(x, y).gameObject.layer = LayerMask.NameToLayer("VisibleGrid");
            }
        }
    }
    public void ClearPath()
    {
        for (int x = 0; x < _grid.Rows; x++)
        {
            for (int y = 0; y < _grid.Columns; y++)
            {
                if (GetGridPlane(x, y).pathGameObject.activeSelf)
                {
                    GetGridPlane(x, y).pathGameObject.SetActive(false);
                }
            }
        }
    }
    public void ClearAttack()
    {
        for (int x = 0; x < _grid.Rows; x++)
        {
            for (int y = 0; y < _grid.Columns; y++)
            {
                if (GetGridPlane(x, y).GetAttackIndicator().activeSelf)
                {
                    GetGridPlane(x, y).GetAttackIndicator().SetActive(false);
                }
            }
        }
    }
}

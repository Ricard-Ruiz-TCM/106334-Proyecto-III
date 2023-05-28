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

    // Unity OnEnable
    void OnEnable() {
        Actor.onDestinationReached += clearGrid;

        Grid2D.onNodeTypeChanged += clearNode;
    }

    // Unity OnDisable
    void OnDisable() {
        Actor.onDestinationReached -= clearGrid;

        Grid2D.onNodeTypeChanged -= clearNode;
    }

    /** Método para instanciar los planos */
    public void instantiateGrid() {
        _grid = GetComponent<Grid2D>();
        _planeMap = new GridPlane[_grid.rows, _grid.columns];
        // Instanciación de los paneles
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                // Posición donde será isntanciado
                Node node = _grid.getNode(x, y);
                Vector3 position = new Vector3(x * _planeSize * _planePfb.transform.localScale.x + _offset, _offset, y * _planeSize * _planePfb.transform.localScale.y + _offset);
                // Instant del prefab
                GridPlane obj = GameObject.Instantiate(_planePfb, position, Quaternion.identity, transform).GetComponent<GridPlane>();
                obj.gameObject.name = "M[" + x + "," + y + "]-" + "W:" + node.walkable;
                RaycastHit raycastHit;
                if (Physics.Raycast(obj.transform.position, -Vector3.up, out raycastHit, Mathf.Infinity, _layer)) {
                    // RePosition del plane, justo encima del mapeado
                    obj.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.1f, raycastHit.point.z);
                }
                // initialization
                obj.setGrid(_grid, node);
                // Assign en el planeMap
                _planeMap[x, y] = obj;
            }
        }
        clearGrid();
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
            return getGridNode(raycastHit.point);
        }
        return null;
    }

    /** Getters de GridPlane */
    public GridPlane getGridPlane(Node node) {
        return getGridPlane(node.x, node.y);
    }
    public GridPlane getGridPlane(int x, int y) {
        int cX = Mathf.Clamp(x, 0, _grid.rows - 1);
        int cY = Mathf.Clamp(y, 0, _grid.columns - 1);
        return _planeMap[cX, cY];
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

    /** Método para localizar el Node más cercano a un objetivo */
    public Node findClosestNode(Node origin, Node target) {
        return findClosestGrid(getGridPlane(origin), getGridPlane(target)).node;
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
    /** Método para localizar el GridPlane más cercano a un objetivo */
    public GridPlane findClosestGridRoc(GridPlane origin, GridPlane target)
    {
        if (Mathf.Abs(origin.node.x - target.node.x) <= Mathf.Abs(origin.node.y - target.node.y))
        {
            if(origin.node.y > target.node.y)
            {
                return getGridPlane(origin.node.x, origin.node.y - 1);
            }
            else
            {
                return getGridPlane(origin.node.x, origin.node.y + 1);
            }
        }
        else
        {
            if (origin.node.x > target.node.x)
            {
                return getGridPlane(origin.node.x - 1, origin.node.y);
            }
            else
            {
                return getGridPlane(origin.node.x + 1, origin.node.y);
            }
        }
    }

    /** Método para mostar un path concreto */
    public void displayPath(List<Node> path, pathMaterial material) {
        for (int i = 0; i < path.Count; i++) {
            if (i < path.Count - 1) {
                GameObject pathObj = getGridPlane(path[i].x, path[i].y).pathGameObject;
                pathObj.SetActive(true);

                if (path[i + 1].x > path[i].x) {
                    pathObj.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (path[i + 1].x < path[i].x) {
                    pathObj.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (path[i + 1].y > path[i].y) {
                    pathObj.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                if (path[i + 1].y < path[i].y) {
                    pathObj.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }


            //displayNode(path[i], material);
        }
    }

    /** Método para mostar un solo nodo */
    public void displayNode(Node node, pathMaterial material) {
        displayNode(node.x, node.y, material);
    }
    public void displayNode(int x, int y, pathMaterial material) {
        getGridPlane(x, y).setMaterial(_materials[(int)material]);
    }

    /** Método para mostar una skill concreta */
    public void displaySkill(skillID id, Node node, pathMaterial material) {
        if (id.Equals(skillID.NONE))
            return;

        Skill sk = uCore.GameManager.GetSkill(id);

        getGridPlane(node).GetAttackIndicator().SetActive(true);

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - sk.areaRange; i <= node.x + sk.areaRange; i++) {
            for (int j = node.y; j < count + node.y; j++) {
                //displayNode(i, j, material);
                if (Stage.Grid.insideGrid(i, j)) {
                    getGridPlane(i, j).GetAttackIndicator().SetActive(true);
                }
            }
            for (int z = node.y - 1; z > node.y - count; z--) {
                if (Stage.Grid.insideGrid(i, z)) {
                    getGridPlane(i, z).GetAttackIndicator().SetActive(true);
                }
            }

            if (totalCount > sk.areaRange) {
                count--;
            } else {
                count++;
            }
            totalCount++;
        }


    }
    public void DisplayMovementRange(Transform from, int range) 
    {

        int count = 1;
        int totalCount = 1;
        int xCount, jCount, zCount;
        Node node = getGridPlane(from.position).node;
        List<Node> gridList = new List<Node>();

        for (int i = node.x - range; i <= node.x + range; i++)
        {
            if (i < 0) xCount = 0;
            else if (i > Stage.Grid.columns) xCount = Stage.Grid.columns;
            else xCount = i;

            for (int j = count + node.y - 1; j < count + node.y; j++)
            {
                if (j < 0) jCount = 0;
                if (j > Stage.Grid.columns) jCount = Stage.Grid.rows;
                else jCount = j;
                if (getGridNode(xCount,jCount).type != Array2DEditor.nodeType.X && Stage.Pathfinder.isAchievable(node, getGridNode(xCount, jCount), range))
                {
                    displayNode(xCount, jCount, pathMaterial.walkable);
                    gridList.Add(getGridNode(xCount, jCount));
                    getGridNode(xCount, jCount).isRangelimit = true;

                    //GetInsideNodes(getGridPlane(i, j).node, node);
                }
                else
                {
                    bool hasFindClosed = false;
                    GridPlane closestPlane = getGridPlane(xCount, jCount);
                    while (!hasFindClosed)
                    {
                        closestPlane = findClosestGridRoc(closestPlane, getGridPlane(node.x, node.y));
                        if (Stage.Pathfinder.isAchievable(node, closestPlane.node, range))
                        {
                            displayNode(closestPlane.node, pathMaterial.walkable);
                            hasFindClosed = true;
                            gridList.Add(closestPlane.node);
                            closestPlane.node.isRangelimit = true;

                            //GetInsideNodes(closestPlane.node, node);
                        }
                    }

                }

            }
            for (int z = node.y - count + 1; z > node.y - count; z--)
            {
                if (z < 0) zCount = 0;
                if (z > Stage.Grid.columns) zCount = Stage.Grid.rows;
                else zCount = z;

                if (getGridNode(xCount, zCount).type != Array2DEditor.nodeType.X && Stage.Pathfinder.isAchievable(node, getGridNode(xCount, zCount), range))
                {
                    displayNode(xCount, zCount, pathMaterial.walkable);
                    gridList.Add(getGridNode(xCount, zCount));
                    getGridNode(xCount, zCount).isRangelimit = true;

                    //GetInsideNodes(getGridPlane(i, z).node, node);
                }
                else
                {
                    bool hasFindClosed = false;
                    GridPlane closestPlane = getGridPlane(xCount, zCount);
                    while (!hasFindClosed)
                    {
                        closestPlane = findClosestGridRoc(closestPlane,getGridPlane(node.x, node.y));
                        if (Stage.Pathfinder.isAchievable(node, closestPlane.node, range))
                        {
                            displayNode(closestPlane.node, pathMaterial.walkable);
                            hasFindClosed = true;
                            gridList.Add(closestPlane.node);
                            closestPlane.node.isRangelimit = true;

                            //GetInsideNodes(closestPlane.node, node);
                        }
                    }

                }
            }
            if (xCount == node.x - range || xCount == node.x + range)
            {
                if (Stage.Grid.insideGrid(i, node.y))
                {
                    displayNode(xCount, node.y, pathMaterial.invisible);
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

        magic33(node, gridList, range);

        //foreach (GridPlane plane in gridList)
        //{
        //    enableBorders(plane, getDirection(getGridNode(from.position), plane.node), range);
        //}

    }
    //private void GetInsideNodes(Node planeNode, Node node)
    //{
    //    List<Node> neightbourList = new List<Node>(Stage.Grid.getNeighbours(planeNode));

    //    foreach (Node item in neightbourList)
    //    {
    //        if (Stage.Grid.insideGrid(item.x, item.y) && item.type != Array2DEditor.nodeType.X)
    //        {
    //            if (planeNode.x > node.x)
    //            {
    //                if (item.x > node.x && item.x < planeNode.x)
    //                {
    //                    getGridPlane(item).isRangelimit = true;
    //                }

    //            }
    //            else if (planeNode.x < node.x)
    //            {
    //                if (item.x < node.x && item.x > planeNode.x)
    //                {
    //                    getGridPlane(item).isRangelimit = true;
    //                }
    //            }

    //            if (planeNode.y > node.y)
    //            {
    //                if (item.y > node.y && item.y < planeNode.y)
    //                {
    //                    getGridPlane(item).isRangelimit = true;
    //                }

    //            }
    //            else if (planeNode.y < node.y)
    //            {
    //                if (item.y < node.y && item.y > planeNode.y)
    //                {
    //                    getGridPlane(item).isRangelimit = true;
    //                }
    //            }
    //        }
            
    //    }
    //}
    private void magic33(Node node, List<Node> nodeList, int range)
    {
        //int count = 1;
        //int totalCount = 1;
        //for (int i = node.x - range; i <= node.x + range; i++)
        //{
        //    for (int j = node.y; j < count + node.y; j++)
        //    {
        //        if (Stage.Grid.insideGrid(i, j))
        //        {
        //            if (IsInPolygon(getGridNode(i, j), nodeList)) 
        //            {
        //                getGridNode(i, j).isRangelimit = true;
        //                displayNode(i, node.y, pathMaterial.skill);
        //            }
        //            else
        //            {
        //                Debug.Log(getGridNode(i,j).x + " " + getGridNode(i,j).y);
        //            }
        //        }
        //    }
        //    for (int z = node.y - 1; z > node.y - count; z--)
        //    {
        //        if (Stage.Grid.insideGrid(i, z))
        //        {
        //            if (IsInPolygon(getGridNode(i, z), nodeList))
        //            {
        //                getGridNode(i, z).isRangelimit = true;
        //                displayNode(i, node.y, pathMaterial.skill);
        //            }
        //        }
        //    }

        //    if (totalCount > range)
        //    {
        //        count--;
        //    }
        //    else
        //    {
        //        count++;
        //    }
        //    totalCount++;
        //}
        foreach (Node item in nodeList)
        {
            Debug.Log(item.x + " " + item.y);
            displayNode(item, pathMaterial.invisible);
        }
    }
    public static bool IsInPolygon(Node testPoint, List<Node> vertices)
    {
        if (vertices.Count < 3) return false;
        bool isInPolygon = false;
        var lastVertex = vertices[vertices.Count - 1];
        foreach (var vertex in vertices)
        {
            if (IsBetween(testPoint.y,lastVertex.y, vertex.y))
            {
                double t = (testPoint.y - lastVertex.y) / (vertex.y - lastVertex.y);
                double x = t * (vertex.x - lastVertex.x) + lastVertex.x;
                if (x >= testPoint.x) isInPolygon = !isInPolygon;
            }
            else
            {
                if (testPoint.y == lastVertex.y && testPoint.x < lastVertex.x && vertex.y > testPoint.y) isInPolygon = !isInPolygon;
                if (testPoint.y == vertex.y && testPoint.x < vertex.x && lastVertex.y > testPoint.y) isInPolygon = !isInPolygon;
            }

            lastVertex = vertex;
        }

        return isInPolygon;
    }

    public static bool IsBetween(double x, double a, double b)
    {
        return (x - a) * (x - b) < 0;
    }
    public void enableBorders(GridPlane plane, Vector2 vec, int maxRange)
    {
        if (vec.x > 0)
            plane.limitLeft.SetActive(true);

        else if (vec.x < 0)
            plane.limitRight.SetActive(true);

        if (vec.y > 0)
            plane.limitDown.SetActive(true);

        else if (vec.y < 0)
            plane.limitUp.SetActive(true);
        //else
        //{
        //    foreach (var item in Stage.Grid.getNeighbours(getGridPlane(vec).node))
        //    {

        //    }
        //}

    }
    /*** Método para ocultar un nodo */
    public void hideNode(Node node) {
        displayNode(node, pathMaterial.invisible);
    }
    public void hideNode(int x, int y) {
        hideNode(getGridNode(x, y));
    }
    public void displaySkillRange(int range, Node node, pathMaterial mat = pathMaterial.skill) {

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - range; i <= node.x + range; i++) {
            for (int j = node.y; j < count + node.y; j++) {
                if (Stage.Grid.insideGrid(i, j)) {
                    displayNode(i, j, mat);
                }
            }
            for (int z = node.y - 1; z > node.y - count; z--) {
                if (Stage.Grid.insideGrid(i, z)) {
                    displayNode(i, z, mat);
                }
            }

            if (totalCount > range) {
                count--;
            } else {
                count++;
            }
            totalCount++;
        }
    }

    public void ClearAttack() {
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                if (getGridPlane(x, y).GetAttackIndicator().activeSelf == true) {
                    getGridPlane(x, y).GetAttackIndicator().SetActive(false);
                }
            }
        }
    }
    public void clearPath() {
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                if (getGridPlane(x, y).pathGameObject.activeSelf == true) {
                    getGridPlane(x, y).pathGameObject.SetActive(false);
                }
            }
        }
    }

    /** Método para limpiar el grid y hacerlo invisible */
    public void clearGrid() {
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                clearNode(x, y);
            }
        }
    }

    /** Métodos para limpiar un nodo concreto y setearlo por defecto */
    public void clearNode(Node node) {
        clearNode(node.x, node.y);
    }
    public void clearNode(int x, int y) {
        Material mat;
        switch (getGridNode(x, y).type) {
            case Array2DEditor.nodeType.__:
                mat = _materials[(int)pathMaterial.walkable];
                break;
            case Array2DEditor.nodeType.P:
                mat = _materials[(int)pathMaterial.skill];
                break;
            case Array2DEditor.nodeType.X:
                mat = _materials[(int)pathMaterial.notWalkable];
                break;
            default:
                mat = _materials[(int)pathMaterial.NONE];
                break;
        }

        getGridPlane(x, y).clear(mat);
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid2D))]
public class GridBuilder : MonoBehaviour {

    public static event Action onGridBuild;

    [HideInInspector]
    public Grid2D _grid;
    private GridPlane[,] _planeMap;

    [SerializeField, Header("Plane Prefab for Grid:")]
    private GameObject _planePfb;
    [SerializeField, Tooltip("gpl = GridPanelSize => Lo que mide el mesh del panel, pues scale 1 => 10")]
    private float _planeSize = 1.5f;
    private float _offset;

    [SerializeField, Header("Terrain Layer:")]
    private LayerMask _layer;

    [SerializeField, Header("Materials:"), Tooltip("Tiene que coincidir con el enum pathMaterial")]
    private Material[] _materials;

    [SerializeField] LayerMask actorMask;
    [SerializeField] LayerMask gridMask;

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

    /** M�todo para instanciar los planos */
    public void instantiateGrid() {

        _offset = _planeSize / 2f;

        _planePfb.transform.localScale = new Vector3(_planeSize / 10f, _planeSize / 10f, _planeSize / 10f);

        _grid = GetComponent<Grid2D>();
        _planeMap = new GridPlane[_grid.rows, _grid.columns];
        // Instanciaci�n de los paneles
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                // Posici�n donde ser� isntanciado
                Node node = _grid.getNode(x, y);
                Vector3 position = new Vector3(x * 10f * _planePfb.transform.localScale.x + _offset, 0.1f, y * 10f * _planePfb.transform.localScale.y + _offset);
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
        onGridBuild?.Invoke();
        clearGrid();
    }

    /** M�todo que compruba si el rat�n est� sobre la Grid2D */
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(uCore.Action.mousePosition), out raycastHit, 100, gridMask)) {
            return getGridNode(raycastHit.point);
        }
        return null;
    }
    public Actor getMouseEnemyOrNPC() {
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(uCore.Action.mousePosition), out raycastHit, 100, actorMask)) {
            if (raycastHit.transform.GetComponent<Actor>()) {
                return raycastHit.transform.GetComponent<Actor>();
            }

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
        return getGridPlane((int)(worldPos.x / _planeSize), (int)(worldPos.z / _planeSize));
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

    /** M�todo para calcualr la distancia */
    private int calcDistance(int x1, int x2, int y1, int y2) {
        return Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2);
    }

    /** M�todo para calcular la direcci�n entre 2 */
    public Vector2 getDirection(Vector3 a, Vector3 b) {
        return getDirection(getGridPlane(a), getGridPlane(b));
    }
    public Vector2 getDirection(GridPlane a, GridPlane b) {
        return getDirection(a.node, b.node);
    }
    public Vector2 getDirection(Node a, Node b) {
        return calcDirection(a.x, b.x, a.y, b.y);
    }

    /** M�todo para calcular la direcci�n */
    private Vector2 calcDirection(int x1, int x2, int y1, int y2) {
        return new Vector2(Mathf.Clamp((x1 - x2), -1, 1), Mathf.Clamp((y1 - y2), -1, 1));
    }

    /** M�todo para localizar el Node m�s cercano a un objetivo */
    public Node findClosestNode(Node origin, Node target) {
        return findClosestGrid(getGridPlane(origin), getGridPlane(target)).node;
    }

    /** M�todo para localizar el GridPlane m�s cercano a un objetivo */
    public GridPlane findClosestGrid(GridPlane origin, GridPlane target) {
        int tmp = 1000;
        Node node = null;
        List<Node> neighbours = _grid.getNeighbours(target.node);
        foreach (Node cell in neighbours) {
            if (cell != target.node && cell.walkable) {
                int dis = getDistance(origin.node, cell);
                if (dis < tmp) {
                    tmp = dis;
                    node = cell;
                }
            }
        }
        return getGridPlane(node);
    }
    /** M�todo para localizar el GridPlane m�s cercano a un objetivo */
    public GridPlane findClosestGridRoc(GridPlane origin, GridPlane target) {
        if (Mathf.Abs(origin.node.x - target.node.x) <= Mathf.Abs(origin.node.y - target.node.y)) {
            if (origin.node.y > target.node.y) {
                return getGridPlane(origin.node.x, origin.node.y - 1);
            } else {
                return getGridPlane(origin.node.x, origin.node.y + 1);
            }
        } else {
            if (origin.node.x > target.node.x) {
                return getGridPlane(origin.node.x - 1, origin.node.y);
            } else {
                return getGridPlane(origin.node.x + 1, origin.node.y);
            }
        }
    }

    /** M�todo para mostar un path concreto */
    public void displayPath(List<Node> path, pathMaterial material,Vector3 pos) 
    {     
        List<Node> pathList = new List<Node>(path.ToArray());
        pathList.Insert(0, getGridNode(pos));
        for (int i = 0; i < pathList.Count; i++) {
            if (i < pathList.Count - 1) {
                GameObject pathObj = getGridPlane(pathList[i].x, pathList[i].y).pathGameObject;
                pathObj.SetActive(true);

                if (pathList[i + 1].x > pathList[i].x) {
                    pathObj.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (pathList[i + 1].x < pathList[i].x) {
                    pathObj.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (pathList[i + 1].y > pathList[i].y) {
                    pathObj.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                if (pathList[i + 1].y < pathList[i].y) {
                    pathObj.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }


            //displayNode(path[i], material);
        }
    }

    /** M�todo para mostar un solo nodo */
    public void displayNode(Node node, pathMaterial material) {
        displayNode(node.x, node.y, material);
    }
    public void displayNode(int x, int y, pathMaterial material) {
        getGridPlane(x, y).setMaterial(_materials[(int)material]);
    }

    /** M�todo para mostar una skill concreta */
    public void displaySkill(skillID id, Node node, pathMaterial material, Actor from) {
        if (id.Equals(skillID.NONE))
            return;

        Skill sk = uCore.GameManager.GetSkill(id);

        getGridPlane(node).GetAttackIndicator().SetActive(true);

        int count = 1;
        int totalCount = 1;
        Node closestNode = Stage.StageBuilder.findClosestNode(Stage.StageBuilder.getGridNode(from.transform.position), node);

        for (int i = node.x - sk.areaRange; i <= node.x + sk.areaRange; i++) {
            for (int j = node.y; j < count + node.y; j++) {
                //displayNode(i, j, material);
                if (Stage.Grid.insideGrid(i, j)) {
                    if (sk.ID == skillID.Cleave) {
                        if (getGridNode(i, j) != closestNode) {
                            getGridPlane(i, j).GetAttackIndicator().SetActive(true);
                        }
                    } else {
                        getGridPlane(i, j).GetAttackIndicator().SetActive(true);
                    }

                }
            }
            for (int z = node.y - 1; z > node.y - count; z--) {
                if (Stage.Grid.insideGrid(i, z)) {
                    if (sk.ID == skillID.Cleave) {
                        if (getGridNode(i, z) != closestNode) {
                            getGridPlane(i, z).GetAttackIndicator().SetActive(true);
                        }
                    } else {
                        getGridPlane(i, z).GetAttackIndicator().SetActive(true);
                    }
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
    public void DisplayMovementRange(Transform from, int range, bool isEnemy)
    {
        int count = 1;
        int totalCount = 1;
        int xCount, jCount, zCount;
        Node node = getGridPlane(from.position).node;
        List<Node> gridList = new List<Node>();
        List<Node> finalGridList = new List<Node>();

        for (int i = node.x - range; i <= node.x + range; i++)
        {
            if (i < 0)
                xCount = 0;
            else if (i > Stage.Grid.rows)
                xCount = Stage.Grid.rows;
            else
                xCount = i;

            for (int j = count + node.y - 1; j < count + node.y; j++)
            {
                if (j < 0)
                    jCount = 0;
                if (j > Stage.Grid.columns)
                    jCount = Stage.Grid.columns;
                else
                    jCount = j;
                if (getGridNode(xCount, jCount).type != Array2DEditor.nodeType.X && Stage.Pathfinder.isAchievable(node, getGridNode(xCount, jCount), range))
                {
                    displayNode(xCount, jCount, pathMaterial.walkable);
                    gridList.Add(getGridNode(xCount, jCount));
                    finalGridList.Add(getGridNode(xCount, jCount));
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
                        if (Stage.Pathfinder.isAchievable(node, closestPlane.node, range) )
                        {
                            displayNode(closestPlane.node, pathMaterial.walkable);
                            hasFindClosed = true;
                            gridList.Add(closestPlane.node);
                            finalGridList.Add(closestPlane.node);
                            closestPlane.node.isRangelimit = true;

                            //GetInsideNodes(closestPlane.node, node);
                        }
                    }

                }

            }
            for (int z = node.y - count + 1; z > node.y - count; z--)
            {
                if (z < 0)
                    zCount = 0;
                if (z > Stage.Grid.columns)
                    zCount = Stage.Grid.columns;
                else
                    zCount = z;

                if (getGridNode(xCount, zCount).type != Array2DEditor.nodeType.X && Stage.Pathfinder.isAchievable(node, getGridNode(xCount, zCount), range))
                {
                    displayNode(xCount, zCount, pathMaterial.walkable);
                    gridList.Add(getGridNode(xCount, zCount));
                    finalGridList.Add(getGridNode(xCount, zCount));
                    getGridNode(xCount, zCount).isRangelimit = true;

                    //GetInsideNodes(getGridPlane(i, z).node, node);
                }
                else
                {
                    bool hasFindClosed = false;
                    GridPlane closestPlane = getGridPlane(xCount, zCount);
                    while (!hasFindClosed)
                    {
                        closestPlane = findClosestGridRoc(closestPlane, getGridPlane(node.x, node.y));
                        if (Stage.Pathfinder.isAchievable(node, closestPlane.node, range))
                        {
                            displayNode(closestPlane.node, pathMaterial.walkable);
                            hasFindClosed = true;
                            gridList.Add(closestPlane.node);
                            finalGridList.Add(closestPlane.node);
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
                    displayNode(xCount, node.y, pathMaterial.walkable);
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

        foreach (Node item in gridList)
        {
            bool hasArrivedCenter = false;
            GridPlane closestPlane = getGridPlane(item);
            while (!hasArrivedCenter)
            {
                closestPlane = findClosestGridRoc(getGridPlane(closestPlane.node), getGridPlane(node)); // si hay algun bug que se pone mal, 100000000000% es por esto uwu
                if (closestPlane.node.type != Array2DEditor.nodeType.X || (closestPlane.node == node && isEnemy))
                {
                    closestPlane.node.isRangelimit = true;
                    //displayNode(closestPlane, pathMaterial.invisible);
                    finalGridList.Add(closestPlane.node);

                    if (closestPlane.node == node)
                    {
                        hasArrivedCenter = true;

                    }
                }
            }
            displayNode(item, pathMaterial.walkable);
        }
        DisplayBorders(finalGridList, true);
        foreach (Node item in finalGridList)
        {
            item.isRangelimit = false;
        }

    }

    public void DisplayBorders(List<Node> nodeList, bool path) {
        foreach (Node item in nodeList) {
            List<Vector2> neightbourList = new List<Vector2>(Stage.Grid.getNeighboursWithoutCheck(item));
            foreach (Vector2 neightbour in neightbourList) {
                if (!getGridNode((int)neightbour.x, (int)neightbour.y).isRangelimit || !Stage.Grid.insideGrid((int)neightbour.x, (int)neightbour.y)) {
                    if (neightbour.x > item.x) {
                        if (path) {
                            getGridPlane(item).limitRight.SetActive(true);
                        } else {
                            getGridPlane(item).limitRightSkill.SetActive(true);
                        }

                    }
                    if (neightbour.x < item.x) {
                        if (path) {
                            getGridPlane(item).limitLeft.SetActive(true);
                        } else {
                            getGridPlane(item).limitLeftSkill.SetActive(true);
                        }
                    }
                    if (neightbour.y > item.y) {
                        if (path) {
                            getGridPlane(item).limitUp.SetActive(true);
                        } else {
                            getGridPlane(item).limitUpSkill.SetActive(true);
                        }
                    }
                    if (neightbour.y < item.y) {
                        if (path) {
                            getGridPlane(item).limitDown.SetActive(true);
                        } else {
                            getGridPlane(item).limitDownSkill.SetActive(true);
                        }
                    }

                }
            }
        }
    }
    /*** M�todo para ocultar un nodo */
    public void hideNode(Node node) {
        displayNode(node, pathMaterial.walkable);
    }
    public void hideNode(int x, int y) {
        hideNode(getGridNode(x, y));
    }
    public List<Node> rangeList(int range, Node node)
    {
        List<Node> skillRangeNodeList = new List<Node>();
        List<Node> rangeListToReturn = new List<Node>();

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - range; i <= node.x + range; i++)
        {
            for (int j = node.y; j < count + node.y; j++)
            {
                if (Stage.Grid.insideGrid(i, j))
                {
                    //displayNode(i, j, mat);
                    skillRangeNodeList.Add(getGridNode(i, j));
                    rangeListToReturn.Add(getGridNode(i, j));
                }
            }
            for (int z = node.y - 1; z > node.y - count; z--)
            {
                if (Stage.Grid.insideGrid(i, z))
                {
                    //displayNode(i, z, mat);
                    skillRangeNodeList.Add(getGridNode(i, z));
                    rangeListToReturn.Add(getGridNode(i, z));
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

        //getSkilleableNodes(skillRangeNodeList, node);

        foreach (Node item in skillRangeNodeList)
        {
            if (item.makeCover)
            {
                List<Node> outNodes = new List<Node>();

                Vector2 dir = getDirection(item, node);

                if (dir.x > 0)
                {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes)
                    {
                        nodeProva = getGridNode(nodeProva.x + 1, nodeProva.y);
                        if (rangeListToReturn.Contains(nodeProva))
                        {
                            rangeListToReturn.Remove(getGridNode(nodeProva.x, nodeProva.y));
                        }
                        else
                        {
                            noMoreNodes = true;
                        }

                    }

                }
                else if (dir.x < 0)
                {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes)
                    {
                        nodeProva = getGridNode(nodeProva.x - 1, nodeProva.y);
                        if (rangeListToReturn.Contains(nodeProva))
                        {
                            rangeListToReturn.Remove(getGridNode(nodeProva.x, nodeProva.y));
                        }
                        else
                        {
                            noMoreNodes = true;
                        }

                    }
                }

                if (dir.y > 0)
                {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes)
                    {
                        nodeProva = getGridNode(nodeProva.x, nodeProva.y + 1);
                        if (rangeListToReturn.Contains(nodeProva))
                        {
                            rangeListToReturn.Remove(getGridNode(nodeProva.x, nodeProva.y));
                            outNodes.Add(getGridNode(nodeProva.x, nodeProva.y));
                        }
                        else
                        {
                            noMoreNodes = true;
                        }

                    }

                }
                else if (dir.y < 0)
                {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes)
                    {
                        nodeProva = getGridNode(nodeProva.x, nodeProva.y - 1);
                        if (rangeListToReturn.Contains(nodeProva))
                        {
                            rangeListToReturn.Remove(getGridNode(nodeProva.x, nodeProva.y));
                            outNodes.Add(getGridNode(nodeProva.x, nodeProva.y));
                        }
                        else
                        {
                            noMoreNodes = true;
                        }

                    }
                }
                if (dir.x != 0 && dir.y != 0)
                {
                    if (dir.x > 0)
                    {
                        foreach (Node outItem in outNodes)
                        {
                            Node nodeProva = outItem;
                            bool noMoreNodes = false;
                            while (!noMoreNodes)
                            {
                                nodeProva = getGridNode(nodeProva.x + 1, nodeProva.y);
                                if (rangeListToReturn.Contains(nodeProva))
                                {
                                    rangeListToReturn.Remove(getGridNode(nodeProva.x, nodeProva.y));
                                }
                                else
                                {
                                    noMoreNodes = true;
                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (Node outItem in outNodes)
                        {
                            Node nodeProva = outItem;
                            bool noMoreNodes = false;
                            while (!noMoreNodes)
                            {
                                nodeProva = getGridNode(nodeProva.x - 1, nodeProva.y);
                                if (rangeListToReturn.Contains(nodeProva))
                                {
                                    rangeListToReturn.Remove(getGridNode(nodeProva.x, nodeProva.y));
                                }
                                else
                                {
                                    noMoreNodes = true;
                                }

                            }
                        }
                    }
                }
            }
        }       

        return rangeListToReturn;
    }
    public void displaySkillRange(int range, Node node, pathMaterial mat = pathMaterial.skill) {
        List<Node> skillRangeNodeList = new List<Node>();

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - range; i <= node.x + range; i++) {
            for (int j = node.y; j < count + node.y; j++) {
                if (Stage.Grid.insideGrid(i, j)) {
                    //displayNode(i, j, mat);
                    skillRangeNodeList.Add(getGridNode(i, j));
                }
            }
            for (int z = node.y - 1; z > node.y - count; z--) {
                if (Stage.Grid.insideGrid(i, z)) {
                    //displayNode(i, z, mat);
                    skillRangeNodeList.Add(getGridNode(i, z));
                }
            }

            if (totalCount > range) {
                count--;
            } else {
                count++;
            }
            totalCount++;
        }

        getSkilleableNodes(skillRangeNodeList, node);
    }

    public void displayBothBordersActive() {
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                GridPlane plane = getGridPlane(x, y);
                if (plane.limitUp.activeSelf && plane.limitUpSkill.activeSelf) {
                    plane.limitUpSkill.GetComponent<MeshRenderer>().material = Resources.Load("Materials/borderMat", typeof(Material)) as Material;
                }
                if (plane.limitDown.activeSelf && plane.limitDownSkill.activeSelf) {
                    plane.limitDownSkill.GetComponent<MeshRenderer>().material = Resources.Load("Materials/borderMat", typeof(Material)) as Material;
                }
                if (plane.limitLeft.activeSelf && plane.limitLeftSkill.activeSelf) {
                    plane.limitLeftSkill.GetComponent<MeshRenderer>().material = Resources.Load("Materials/borderMat", typeof(Material)) as Material;
                }
                if (plane.limitRight.activeSelf && plane.limitRightSkill.activeSelf) {
                    plane.limitRightSkill.GetComponent<MeshRenderer>().material = Resources.Load("Materials/borderMat", typeof(Material)) as Material;
                }
            }
        }
    }
    private List<Node> getSkilleableNodes(List<Node> nodeList, Node origin) {
        List<Node> skilleableNodes = new List<Node>(nodeList.ToArray());

        foreach (Node item in nodeList) {
            if (item.makeCover) {
                List<Node> outNodes = new List<Node>();

                Vector2 dir = getDirection(item, origin);

                if (dir.x > 0) {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes) {
                        nodeProva = getGridNode(nodeProva.x + 1, nodeProva.y);
                        if (skilleableNodes.Contains(nodeProva)) {
                            skilleableNodes.Remove(getGridNode(nodeProva.x, nodeProva.y));
                        } else {
                            noMoreNodes = true;
                        }

                    }

                } else if (dir.x < 0) {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes) {
                        nodeProva = getGridNode(nodeProva.x - 1, nodeProva.y);
                        if (skilleableNodes.Contains(nodeProva)) {
                            skilleableNodes.Remove(getGridNode(nodeProva.x, nodeProva.y));
                        } else {
                            noMoreNodes = true;
                        }

                    }
                }

                if (dir.y > 0) {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes) {
                        nodeProva = getGridNode(nodeProva.x, nodeProva.y + 1);
                        if (skilleableNodes.Contains(nodeProva)) {
                            skilleableNodes.Remove(getGridNode(nodeProva.x, nodeProva.y));
                            outNodes.Add(getGridNode(nodeProva.x, nodeProva.y));
                        } else {
                            noMoreNodes = true;
                        }

                    }

                } else if (dir.y < 0) {
                    Node nodeProva = item;
                    bool noMoreNodes = false;
                    while (!noMoreNodes) {
                        nodeProva = getGridNode(nodeProva.x, nodeProva.y - 1);
                        if (skilleableNodes.Contains(nodeProva)) {
                            skilleableNodes.Remove(getGridNode(nodeProva.x, nodeProva.y));
                            outNodes.Add(getGridNode(nodeProva.x, nodeProva.y));
                        } else {
                            noMoreNodes = true;
                        }

                    }
                }
                if (dir.x != 0 && dir.y != 0) {
                    if (dir.x > 0) {
                        foreach (Node outItem in outNodes) {
                            Node nodeProva = outItem;
                            bool noMoreNodes = false;
                            while (!noMoreNodes) {
                                nodeProva = getGridNode(nodeProva.x + 1, nodeProva.y);
                                if (skilleableNodes.Contains(nodeProva)) {
                                    skilleableNodes.Remove(getGridNode(nodeProva.x, nodeProva.y));
                                } else {
                                    noMoreNodes = true;
                                }

                            }
                        }
                    } else {
                        foreach (Node outItem in outNodes) {
                            Node nodeProva = outItem;
                            bool noMoreNodes = false;
                            while (!noMoreNodes) {
                                nodeProva = getGridNode(nodeProva.x - 1, nodeProva.y);
                                if (skilleableNodes.Contains(nodeProva)) {
                                    skilleableNodes.Remove(getGridNode(nodeProva.x, nodeProva.y));
                                } else {
                                    noMoreNodes = true;
                                }

                            }
                        }
                    }
                }
            }
        }

        // muetra lista
        foreach (Node item in skilleableNodes) {
            displayNode(item, pathMaterial.skill);
            getGridPlane(item).setValid2Attack();
            item.isRangelimit = true;
        }

        DisplayBorders(skilleableNodes, false);

        foreach (Node item in skilleableNodes) {
            item.isRangelimit = false;
        }
        return skilleableNodes;
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

    /** M�todo para limpiar el grid y hacerlo invisible */
    public void clearGrid() {
        for (int x = 0; x < _grid.rows; x++) {
            for (int y = 0; y < _grid.columns; y++) {
                clearNode(x, y);
            }
        }
    }

    /** M�todos para limpiar un nodo concreto y setearlo por defecto */
    public void clearNode(Node node) {
        clearNode(node.x, node.y);
    }
    public void clearNode(int x, int y) {
        Material mat;
        switch (getGridNode(x, y).type) {
            case Array2DEditor.nodeType.__:
                mat = _materials[(int)pathMaterial.walkable];
                break;
            case Array2DEditor.nodeType.C:
                mat = _materials[(int)pathMaterial.cover];
                break;
            case Array2DEditor.nodeType.P:
                mat = _materials[(int)pathMaterial.positioning];
                break;
            case Array2DEditor.nodeType.X:
                mat = _materials[(int)pathMaterial.notWalkable];
                break;
            default:
                mat = _materials[(int)pathMaterial.walkable];
                break;
        }
        getGridPlane(x, y).clear(mat);
    }

    public void removePNodes() {
        for (int i = 0; i < 10; i++) {
            GridPlane plane = Stage.StageManager.findPositionNode();
            if (plane != null) {
                Stage.Grid.changeNodeType(plane.transform.position, Array2DEditor.nodeType.__);
            }
        }
    }

}

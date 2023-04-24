using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Grid2D))]
public class GridBuilder : MonoBehaviour {


    private Grid2D _grid;
    private GridPlane[,] _planeMap;

    [SerializeField, Header("Plane Prefab for Grid:")]
    private GameObject _planePfb;
    // gpl = GridPanelSize => Lo que mide el mesh del panel, pues scale 1 => 10
    public float _planeSize = 10f;
    
    [SerializeField, Header("Terrain Layer:")]
    private LayerMask _layer;

    [SerializeField, Header("Materials_")]
    public Material _walkableMat;
    public Material _unwalkableMat;
    public Material _pathMath;
    public Material _rangeMath;

    // Unity Awake
    void Awake() {
        _grid = GetComponent<Grid2D>();
        _planeMap = new GridPlane[_grid.Rows, _grid.Columns];
    }

    // Unity Start
    void Start() {
        // Instanciación de los paneles
        for (int x = 0; x < _grid.Rows; x++) {
            for (int y = 0; y < _grid.Columns; y++) {
                // Posición donde será isntanciado
                Vector3 position = new Vector3(x * _planeSize * _planePfb.transform.localScale.x, 50f, y * _planeSize * _planePfb.transform.localScale.y);
                // Instant del prefab
                GridPlane obj = GameObject.Instantiate(_planePfb, position, Quaternion.identity, transform).GetComponent<GridPlane>();
                obj.gameObject.name = "M[" + x + "," + y + "]-" + "W:" + _grid.GetNode(x,y).walkable;
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

    // Update del material del Plane
    public void UpdateMaterial(int x, int y, Material mat = null) {
        if (mat == null) {
            mat = (_grid.GetNode(x, y).walkable ? _walkableMat : _unwalkableMat);
        }
        GetGridPlane(x, y).SetMaterial(mat);
    }

    /** Método para cambiar el path de colores */
    public void DisplayPath(List<Node> path) {
        for (int x = 0; x < _grid.Rows; x++) {
            for (int y = 0; y < _grid.Columns; y++) {
                UpdateMaterial(x, y);
            }
        }
        foreach (Node nd in path) {
            UpdateMaterial(nd.x, nd.y, _pathMath);
        }
    }
}

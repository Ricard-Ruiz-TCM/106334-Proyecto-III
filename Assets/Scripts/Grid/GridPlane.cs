using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GridPlane : MonoBehaviour {

    // MeshRenderer para cambiar el mat
    private MeshRenderer _meshRenderer;

    private GameObject attackShow;

    // Node & Grid
    public Node node;
    public Grid2D grid2D;
    public GameObject pathGameObject;

    public GameObject limitLeft;
    public GameObject limitRight;
    public GameObject limitUp;
    public GameObject limitDown;

    // Fast position access
    public Vector3 position {
        get {
            return transform.position;
        }
    }

    // Unity Awake
    void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
        attackShow = transform.GetChild(1).gameObject;
    }

    /** Método set de Grid y Nodo */
    public void setGrid(Grid2D grid, Node node) {
        grid2D = grid;
        this.node = node;
    }

    /** Método para setear el material */
    public void setMaterial(Material mat) {
        _meshRenderer.material = mat;
    }

    public Material GetMaterial() {
        return _meshRenderer.material;
    }

    public bool CompareMaterial(Material mat) {
        return _meshRenderer.material == mat;
    }

    public GameObject GetAttackIndicator() {
        return attackShow;
    }


    public void ClearBorders() {
        limitLeft.SetActive(false);
        limitRight.SetActive(false);
        limitUp.SetActive(false);
        limitDown.SetActive(false);
    }

    public void clear(Material mat) {
        clearPath();
        ClearBorders();
        setMaterial(mat);
        clearAttackIndicator();
    }

    public void clearAttackIndicator() {
        attackShow.SetActive(false);
    }

    public void clearPath() {
        pathGameObject.SetActive(false);
    }
}

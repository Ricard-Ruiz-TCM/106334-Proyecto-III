using UnityEngine;

public class GridPlane : MonoBehaviour {

    // MeshRenderer para cambiar el mat
    private MeshRenderer _meshRenderer;

    private GameObject attackShow;

    // Node & Grid
    public Node node;
    public Grid2D grid2D;

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

    /** M�todo set de Grid y Nodo */
    public void SetGrid(Grid2D grid, Node node) {
        grid2D = grid;
        this.node = node;
    }

    /** M�todo para setear el material */
    public void SetMaterial(Material mat) {
        _meshRenderer.material = mat;
    }
    public Material GetMaterial()
    {
        return _meshRenderer.material;
    }
    public GameObject GetAttackIndicator()
    {
        return attackShow;
    }

}

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
    public void setGrid(Grid2D grid, Node node) {
        grid2D = grid;
        this.node = node;
    }

    /** M�todo para establecer el material y layer */
    public void setRendering(Material mat, string layer) {
        setLayer(layer);
        setMaterial(mat);
    }

    /** M�todo para setear el material */
    public void setMaterial(Material mat) {
        _meshRenderer.material = mat;
    }
    public Material GetMaterial()
    {
        return _meshRenderer.material;
    }
    public bool CompareMaterial(Material mat)
    {
        return _meshRenderer.material == mat;
    }
    public GameObject GetAttackIndicator()
    {
        return attackShow;
    }

    /** M�todo para establecer el layer */
    public void setLayer(string layer) {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

}

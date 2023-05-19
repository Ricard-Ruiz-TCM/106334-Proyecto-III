using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GridPlane : MonoBehaviour {

    // MeshRenderer para cambiar el mat
    private MeshRenderer _meshRenderer;

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

    /** M�todo para establecer el layer */
    public void setLayer(string layer) {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

}

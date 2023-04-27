using UnityEngine;

public class GridPlane : MonoBehaviour
{

    // MeshRenderer para cambiar el mat
    private MeshRenderer _meshRenderer;

    // Node & Grid
    public Node node;
    public Grid2D grid2D;

    // Fast position access
    public Vector3 position
    {
        get { return transform.position; }
    }

    // Unity Awake
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    /** Método set de Grid y Nodo */
    public void SetGrid(Grid2D grid, Node node)
    {
        grid2D = grid; this.node = node;
    }

    /** Método para setear el material */
    public void SetMaterial(Material mat)
    {
        _meshRenderer.material = mat;
    }

}

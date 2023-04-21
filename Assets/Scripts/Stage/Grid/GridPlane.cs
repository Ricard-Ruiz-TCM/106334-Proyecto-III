using UnityEngine;

public class GridPlane : MonoBehaviour {

    private MeshRenderer _meshRenderer;

    [HideInInspector]
    public Grid grid;

    public Node node;

    public Vector3 position {
        get {
            return transform.position;
        }
    }

    private void Awake() {
        grid = FindObjectOfType<Grid>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Set(Grid gr, Node nd) {
        grid = gr;
        node = nd;
    }

    public void SetMaterial(Material mat) {
        _meshRenderer.material = mat;
    }



}

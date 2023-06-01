using UnityEngine;

public class GridPlane : MonoBehaviour {

    public GameObject attackShow;

    // Node & Grid
    public Node node;
    public Grid2D grid2D;
    public GameObject pathGameObject;

    public GameObject limitLeft;
    public GameObject limitRight;
    public GameObject limitUp;
    public GameObject limitDown;

    private bool _canBeAttacked = false;
    public bool CanBeAttacked => _canBeAttacked;

    public MeshRenderer _interior;


    public void setValid2Attack() {
        _canBeAttacked = true;
    }

    // Fast position access
    public Vector3 position {
        get {
            return transform.position;
        }
    }

    /** Método set de Grid y Nodo */
    public void setGrid(Grid2D grid, Node node) 
    {
        grid2D = grid;
        this.node = node;
    }

    /** Método para setear el material */
    public void setMaterial(Material mat) {
        _interior.material = mat;
    }

    public Material GetMaterial() {
        return _interior.material;
    }

    public bool CompareMaterial(Material mat) {
        return _interior.material == mat;
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
        _canBeAttacked = false;
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

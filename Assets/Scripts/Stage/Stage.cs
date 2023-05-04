using UnityEngine;
using System.Collections.Generic;

public class Stage : MonoBehaviour {

    public int _difficulty;
    public List<GameObject> _enemies;

    private Grid2D _grid;

    public static Grid2D StageGrid = null;

    // Unity Awake
    void Awake() {
        _grid = transform.GetComponentInChildren<Grid2D>();

        Stage.StageGrid = _grid;
    }

    // Unity StartStart
    void Start() {
        foreach (GameObject a in _enemies) {
            GameObject.Instantiate(a);
        }
    }

}

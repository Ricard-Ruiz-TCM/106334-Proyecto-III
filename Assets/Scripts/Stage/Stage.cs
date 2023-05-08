using UnityEngine;
using System.Collections.Generic;

public class Stage : MonoBehaviour {

    public StageData _data;
    public List<GameObject> _enemies;

    public static Grid2D StageGrid = null;
    public static GridBuilder StageBuilder = null;
    public static GridManager StageManager = null;

    // Unity Awake
    void Awake() {
        Stage.StageGrid = transform.GetComponentInChildren<Grid2D>();
        Stage.StageBuilder = transform.GetComponentInChildren<GridBuilder>();
        Stage.StageManager = transform.GetComponentInChildren<GridManager>();
    }

    // Unity StartStart
    void Start() {
        foreach (GameObject a in _enemies) {
            GameObject.Instantiate(a);
        }
    }

}

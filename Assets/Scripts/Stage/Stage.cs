using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    [SerializeField, Header("Info:")]
    private StageData _data;
    public StageData Data => _data;

    [SerializeField, Header("Enemies on Stage:")]
    private List<GameObject> _enemies;

    private Player _player;

    public static Grid2D StageGrid = null;
    public static GridBuilder StageBuilder = null;
    public static GridManager StageManager = null;
    public static Pathfinding Pathfinder = null;

    // Unity Awake
    void Awake() {
        Stage.StageGrid = transform.GetComponentInChildren<Grid2D>();
        Stage.StageBuilder = transform.GetComponentInChildren<GridBuilder>();
        Stage.StageManager = transform.GetComponentInChildren<GridManager>();
        Stage.Pathfinder = transform.GetComponentInChildren<Pathfinding>();
    }

    // Unity StartStart
    void Start() {
        foreach (GameObject a in _enemies) {
            GameObject.Instantiate(a);
        }
    }

    /** Método set d ela informaicón */
    public void SetData(StageData data, Player player) {
        _data = data; _player = player;
    }

}

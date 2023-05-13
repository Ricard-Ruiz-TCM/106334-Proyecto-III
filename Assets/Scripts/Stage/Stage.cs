using System;
using UnityEngine;

public class Stage : MonoBehaviour {

    /** Callback para indicar como se completa el stage */
    public static event Action<stageResolution> OnCompleteStage;

    [SerializeField, Header("Info:")]
    private StageData _data;
    public StageData Data => _data;

    /** Static "Unique" access to GridManager and extras, works like Singletons */
    public static Grid2D StageGrid = null;
    public static GridBuilder StageBuilder = null;
    public static GridManager StageManager = null;
    public static Pathfinding Pathfinder = null;
    public static Action CheckStage = null;

    // Unity Awake
    void Awake() {
        Stage.CheckStage = CheckCompletation;
        Stage.StageGrid = transform.GetComponentInChildren<Grid2D>();
        Stage.StageBuilder = transform.GetComponentInChildren<GridBuilder>();
        Stage.StageManager = transform.GetComponentInChildren<GridManager>();
        Stage.Pathfinder = transform.GetComponentInChildren<Pathfinding>();
    }

    // Unity StartStart
    void Start() {
        foreach (EnemyPositionPair epp in _data.enemies) {
            GameObject e = GameObject.Instantiate(epp.enemy);
            e.transform.position = Stage.StageBuilder.GetGridPlane(epp.position.x, epp.position.y).transform.position;
        }
    }

    /** Método set d ela informaicón */
    public void SetData(StageData data) {
        _data = data;
    }

    /** Método para comprobar si el stage ha sido completado, depende del objetivo */
    public void CheckCompletation() {
        bool completed = true;
        switch (_data.objetive) {
            case stageObjetive.killAll:
                foreach (Actor actor in TurnManager.instance.Actors) {
                    if (actor is Enemy) {
                        completed = false;
                    }
                }
                break;
            case stageObjetive.protectNPC:
                Debug.Log("TAMOS JODIDOS PERO CON EL NPC");
                break;
            case stageObjetive.clearPath:
                Debug.Log("TAMOS JODIDOS");
                break;
        }

        if (completed) {
            OnCompleteStage?.Invoke(stageResolution.victory);
        }
    }

}

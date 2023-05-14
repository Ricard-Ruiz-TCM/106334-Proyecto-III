using System;
using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(C_PlaceEnemy(epp.position, GameObject.Instantiate(epp.enemy)));
        }
    }

    /** M�todo Coroutine Para posicionar enemigos */
    private IEnumerator C_PlaceEnemy(Vector2Int position, GameObject enemy) {
        yield return null;
        enemy.transform.position = Stage.StageBuilder.GetGridPlane(position.x, position.y).transform.position;
    }

    /** M�todo set d ela informaic�n */
    public void SetData(StageData data) {
        _data = data;
    }

    /** M�todo para comprobar si el stage ha sido completado, depende del objetivo */
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

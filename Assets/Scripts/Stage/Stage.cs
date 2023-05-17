using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    /** Callback para indicar como se completa el stage */
    public static event Action<stageResolution> onCompleteStage;

    [SerializeField, Header("Info:")]
    private StageData _data;
    public StageData Data => _data;

    [SerializeField, Header("Tiempos:")]
    private float _placeDelay = 2f;

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

    // Unity Start
    void Start() {
        // Instantiate de los enemigos
        foreach (EnemyPositionPair epp in _data.enemies) {
            StartCoroutine(C_PlaceEnemy(epp.position, GameObject.Instantiate(epp.enemy)));
        }
    }

    /** Método Coroutine Para posicionar enemigos */
    private IEnumerator C_PlaceEnemy(Vector2Int position, GameObject enemy) {
        yield return new WaitForSeconds(_placeDelay);
        enemy.transform.position = Stage.StageBuilder.GetGridPlane(position.x, position.y).transform.position;
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
                foreach (Turnable actor in TurnManager.instance.Attenders) {
                    /*if (actor is Enemy) {
                        completed = false;
                    }*/
                }
                break;
            case stageObjetive.protectNPC:
                // TODO // Check si esta el NPC a salvo
                break;
        }

        if (completed) {
            onCompleteStage?.Invoke(stageResolution.victory);
        }
    }

}

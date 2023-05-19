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

    /** Static "Unique" access to GridManager and extras, works like Singletons */
    public static Grid2D Grid = null;
    public static GridBuilder StageBuilder = null;
    public static GridManager StageManager = null;
    public static Pathfinding Pathfinder = null;

    // Unity Awake
    void Awake() {
        Stage.Grid = transform.GetComponentInChildren<Grid2D>();
        Stage.StageBuilder = transform.GetComponentInChildren<GridBuilder>();
        Stage.StageManager = transform.GetComponentInChildren<GridManager>();
        Stage.Pathfinder = transform.GetComponentInChildren<Pathfinding>();
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
                foreach (Turnable actor in TurnManager.instance.attenders) {
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

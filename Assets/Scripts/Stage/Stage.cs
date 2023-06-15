using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    /** Callback para indicar como se completa el stage */
    public static event Action<stageResolution> onCompleteStage;

    /** StageData */
    private StageData _data;
    public StageData Data => _data;

    /** Static "Unique" access to GridManager and extras, works like Singletons */
    public static Grid2D Grid = null;
    public static GridBuilder StageBuilder = null;
    public static GridManager StageManager = null;
    public static Pathfinding Pathfinder = null;

    // Unity OnEnable
    void OnEnable() {
        TurnManager.onModifyAttenders += checkGameResolution;
    }

    // Unity OnDisable
    void OnDisable() {
        TurnManager.onModifyAttenders -= checkGameResolution;
    }

    // Unity Awake
    void Awake() {
        Stage.Grid = transform.GetComponentInChildren<Grid2D>();
        Stage.StageBuilder = transform.GetComponentInChildren<GridBuilder>();
        Stage.StageManager = transform.GetComponentInChildren<GridManager>();
        Stage.Pathfinder = transform.GetComponentInChildren<Pathfinding>();
    }

    /** Método set de la informaicón */
    public void SetData(StageData data) {
        _data = data;
    }

    private bool completed = false;

    /** Método para comprobar si el stage ha sido completado, depende del objetivo */
    private void checkGameResolution() {
        // Si no es combate, no comprobamos el rsolution
        if (!TurnManager.instance.isCombatStarted())
            return;

        if (completed)
            return;

        completed = false;

        bool player = false;
        List<Turnable> players = TurnManager.instance.attenders.FindAll(x => x is ManualActor);
        foreach (Turnable turnables in players) {
            if (turnables.name == "Player") {
                player = true;
            }
        }

        bool npc = TurnManager.instance.attenders.Exists(x => x is ProtectedActor);
        bool enemies = TurnManager.instance.attenders.Exists(x => x is AutomaticActor);

        if (!player) {
            StartCoroutine(endStage(stageResolution.defeat));
        } else {
            switch (_data.objetive) {
                case stageObjetive.killAll:
                    if (!enemies) {
                        completed = true;
                    }
                    break;
                case stageObjetive.protectNPC:
                    if (!npc) {
                        StartCoroutine(endStage(stageResolution.defeat));
                    } else if (!enemies) {
                        completed = TurnManager.instance.attenders.Exists(x => x is ProtectedActor);
                    }
                    break;
            }

            if (completed) {
                StartCoroutine(endStage(stageResolution.victory));
            }
        }
    }

    public float _resolutionDelayInSec = 2.5f;

    private IEnumerator endStage(stageResolution res) {
        TurnManager.instance.endManager();
        yield return new WaitForSeconds(_resolutionDelayInSec);
        onCompleteStage?.Invoke(res);
    }

}

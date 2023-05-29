using System;
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
        TurnManager.instance.onEndRound += (roundType r) => {
            if (!r.Equals(roundType.positioning))
                return;

            TurnManager.instance.onModifyAttenders += checkGameResolution;
        };
    }

    // Unity OnDisable
    void OnDisable() {
        TurnManager.instance.onEndRound -= (roundType r) => {
            if (!r.Equals(roundType.positioning))
                return;

            TurnManager.instance.onModifyAttenders += checkGameResolution;
        };
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

    /** Método para comprobar si el stage ha sido completado, depende del objetivo */
    private void checkGameResolution() {

        bool completed = false;
        bool palyer = TurnManager.instance.attenders.Exists(x => x is ManualActor);
        bool npc = TurnManager.instance.attenders.Exists(x => x is ProtectedActor);
        bool enemies = TurnManager.instance.attenders.Exists(x => x is AutomaticActor);

        if (!palyer) {
            endStage(stageResolution.defeat);
        } else {
            switch (_data.objetive) {
                case stageObjetive.killAll:
                    if (!enemies) {
                        completed = true;
                    }
                    break;
                case stageObjetive.protectNPC:
                    if (!npc) {
                        endStage(stageResolution.defeat);
                    } else if (!enemies) {
                        completed = TurnManager.instance.attenders.Exists(x => x is ProtectedActor);
                    }
                    break;
            }

            if (completed) {
                endStage(stageResolution.victory);
            }
        }
    }

    private void endStage(stageResolution res) {
        TurnManager.instance.endManager();
        onCompleteStage?.Invoke(res);
    }

}

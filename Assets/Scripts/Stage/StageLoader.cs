using UnityEngine;

public class StageLoader : MonoBehaviour {


    [SerializeField, Header("Stage Path:")]
    private string _stagePath;
    [SerializeField]
    private string _stageName;

    [SerializeField, Header("Stage:")]
    private Stage _stage;

    // Unity Start
    void Start() {
        BuildStage();
        BuildPlayer();

        TurnManager.instance.startManager();
    }

    /** M�todo para instanciar el stage seg�n el progreso del juego */
    public void BuildStage() {

        StageData stage = uCore.GameManager.NextStage;

        switch (stage.type) {
            case stageType.combat:
                break;
            case stageType.comrade:
                break;
            case stageType.blacksmith:
                break;
            case stageType.campfire:
                break;
        }
    }

    /** M�todo para instanciar al player con todas sus costias dentro del nivel */
    public void BuildPlayer() {
        
    }

    /** M�todo para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

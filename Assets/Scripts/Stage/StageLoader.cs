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
        GameObject g = Resources.Load<GameObject>(_stagePath + _stageName + uCore.GameManager.StageProgress().ToString());
        _stage = GameObject.Instantiate(g).GetComponent<Stage>();
    }

    /** M�todo para instanciar al player con todas sus costias dentro del nivel */
    public void BuildPlayer() {
        
    }

    /** M�todo para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

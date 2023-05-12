using UnityEngine;

public class StageLoader : MonoBehaviour {

    /** Singleton Instance */
    public static StageLoader instance = null;

    [SerializeField, Header("Stage Path:")]
    private string _stagePath;
    [SerializeField]
    private string _stageName;

    /** Stage */
    private Stage _stage;

    // Unity Awake
    void Awake() {
        // Singleton
        if ((instance != null) && (instance != this)) {
            GameObject.Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    // TESTING
    [Header("TESTING:")]
    public GameObject player, stage;

    /** Método para consturi todo el stage */
    public void buildStage(StageData data) {
        //BuildLevel(data);
        //BuildPlayer();

        // TESTING
        player.SetActive(true);
        stage.SetActive(true);

        // TESTING
        _stage = stage.GetComponent<Stage>();
        GameObject.FindObjectOfType<CameraController>()._target = player.transform;
    }

    /** Método para instanciar el stage según el progreso del juego */
    private void BuildLevel(StageData data) {
        GameObject g = Resources.Load<GameObject>(_stagePath + _stageName + data.ID.ToString());
        _stage = GameObject.Instantiate(g).GetComponent<Stage>();
        _stage.SetData(data);
    }

    /** Método para instanciar al player con todas sus costias dentro del nivel */
    private void BuildPlayer() {
        GameObject g = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Actors/Player"));
        GameObject.FindObjectOfType<CameraController>()._target = g.transform;
    }

    /** Método para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

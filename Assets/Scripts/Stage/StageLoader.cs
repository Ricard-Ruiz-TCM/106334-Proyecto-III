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

    /** M�todo para consturi todo el stage */
    public void buildStage(StageData data) {
        BuildLevel(data);
        BuildPlayer();
    }

    /** M�todo para instanciar el stage seg�n el progreso del juego */
    private void BuildLevel(StageData data) {
        GameObject g = Resources.Load<GameObject>(_stagePath + _stageName + data.ID.ToString());
        _stage = GameObject.Instantiate(g).GetComponent<Stage>();
        _stage.SetData(data);
    }

    /** M�todo para instanciar al player con todas sus costias dentro del nivel */
    private void BuildPlayer() {
        // TODO
    }

    /** M�todo para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

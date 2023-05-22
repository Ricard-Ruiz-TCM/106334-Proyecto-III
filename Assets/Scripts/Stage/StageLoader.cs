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

    // Unity Start

    /** Método para consturi todo el stage */
    public void buildStage(StageData data) {
        _stage = GameObject.Instantiate(Resources.Load<GameObject>(_stagePath + _stageName + data.ID.ToString())).GetComponent<Stage>();
        
    }

}

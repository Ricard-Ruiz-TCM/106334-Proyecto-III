using UnityEngine;

public class StageLoader : MonoBehaviour {

    /** Singleton Instance */
    public static StageLoader instance = null;

    [SerializeField, Header("Stage Path:")]
    private string _stagePath;
    [SerializeField]
    private string _stageName;

    [SerializeField, Header("Stage:")]
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

    /** M�todo para instanciar el stage seg�n el progreso del juego */
    public void BuildStage() {

    }

    /** M�todo para instanciar al player con todas sus costias dentro del nivel */
    public void BuildPlayer() {
        
    }

    /** M�todo para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

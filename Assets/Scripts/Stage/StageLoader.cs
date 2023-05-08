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

    /** Método para consturi todo el stage */
    public void buildStage() {
        BuildLevel();
        BuildPlayer();
    }

    /** Método para instanciar el stage según el progreso del juego */
    private void BuildLevel() {

    }

    /** Método para instanciar al player con todas sus costias dentro del nivel */
    private void BuildPlayer() {
        
    }

    /** Método para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

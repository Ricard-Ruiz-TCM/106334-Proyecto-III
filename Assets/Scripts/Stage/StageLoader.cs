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
    }

    /** Método para instanciar el stage según el progreso del juego */
    public void BuildStage() {
        GameObject g = Resources.Load<GameObject>(_stagePath + _stageName + uCore.GameManager.StageProgress().ToString());
        _stage = GameObject.Instantiate(g).GetComponent<Stage>();
    }

    /** Método para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

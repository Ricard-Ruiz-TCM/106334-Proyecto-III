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

    [SerializeField, Header("Player:")]
    private GameObject _playerPrfb;
    private Player _player;

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
        _stage = GameObject.Instantiate(Resources.Load<GameObject>(_stagePath + _stageName + data.ID.ToString())).GetComponent<Stage>();
        _stage.SetData(data);
    }

    /** M�todo para instanciar al player con todas sus costias dentro del nivel */
    private void BuildPlayer() {
        _player = GameObject.Instantiate(_playerPrfb).GetComponent<Player>();
        uCore.GameManager.RestorePlayer(_player);

        FindObjectOfType<CameraController>()._target = _player.transform;
    }

    /** M�todo para recuperar el stage actual */
    public Stage CurrentStage() {
        return _stage;
    }

}

using UnityEngine;

public class LoadingSceneController : MonoBehaviour {

    [SerializeField]
    private GameObject _inputPanel;
    [SerializeField]
    private GameObject _loadingPanel;

    private bool _sceneLoaded = false;

    // Unity OnEnable
    void OnEnable() {
        SceneDirector.OnSceneLoaded += OnSceneLoaded;
    }

    // Unity OnDisable
    void OnDisable() {
        SceneDirector.OnSceneLoaded -= OnSceneLoaded;
    }

    // Unity Update
    void Update() {
        if (_sceneLoaded) {
            if (uCore.Action.Configured()) {
                uCore.Director.AllowScene();
            }
        }
    }

    // OnSceneLoaded Callback observer
    private void OnSceneLoaded() {
        // Innitial input configured
        if (!uCore.Action.Configured()) {
            _inputPanel.SetActive(true);
            _loadingPanel.SetActive(false);
        }
        _sceneLoaded = true;
    }

}

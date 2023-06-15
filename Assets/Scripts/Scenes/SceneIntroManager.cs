using UnityEngine;

public class SceneIntroManager : MonoBehaviour {

    [SerializeField]
    private GameObject _inputPanel;

    /** Loaded */
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
            if (uCore.Action.isInputConfigured()) {
                FadeFX.instance.FadeIn(() => { uCore.Director.AllowScene(); });
            }
        }
    }

    /** Método para callback del cargado de escenas */
    private void OnSceneLoaded() {
        // Innitial input configured
        if (!uCore.Action.isInputConfigured()) {
            _inputPanel.SetActive(true);
        }
        _sceneLoaded = true;
    }

}

using UnityEngine;

public class LoadingSceneController : MonoBehaviour {

    [SerializeField]
    private GameObject _inputPanel;
    [SerializeField]
    private GameObject _loadingPanel;

    private bool _waitForInput = false;

    // Unity OnEnable
    void OnEnable() {
        SceneDirector.OnSceneLoaded += WaitInput;
    }

    // Unity OnDisable
    void OnDisable() {
        SceneDirector.OnSceneLoaded -= WaitInput;
    }

    // Unity Update
    void Update() {
        if (_waitForInput) {
            if (uCore.Action.GetKeyDown(KeyCode.Space)) {
                uCore.Director.AllowScene();
            }
        }
    }

    private void WaitInput() {
        _inputPanel.SetActive(true);
        _loadingPanel.SetActive(false);
        _waitForInput = true;
    }



}

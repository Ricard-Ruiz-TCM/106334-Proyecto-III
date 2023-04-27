using UnityEngine;

public class IntroScene : MonoBehaviour
{

    [SerializeField]
    private GameObject _inputPanel;

    private bool _sceneLoaded = false;

    // Unity OnEnable
    void OnEnable()
    {
        SceneDirector.OnSceneLoaded += OnSceneLoaded;
    }

    // Unity OnDisable
    void OnDisable()
    {
        SceneDirector.OnSceneLoaded -= OnSceneLoaded;
    }

    // Unity Update
    void Update()
    {
        if (_sceneLoaded)
        {
            if (uCore.Action.isInputConfigured())
            {
                uCore.Director.AllowScene();
            }
        }
    }

    // OnSceneLoaded Callback observer
    private void OnSceneLoaded()
    {
        // Innitial input configured
        if (!uCore.Action.isInputConfigured())
        {
            _inputPanel.SetActive(true);
        }
        _sceneLoaded = true;
    }

}

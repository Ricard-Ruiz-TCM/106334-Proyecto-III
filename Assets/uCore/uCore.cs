using UnityEngine;
using UnityEngine.InputSystem;

public class uCore : MonoBehaviour {

    private static string _preFix = "** -> ? ?? ";
    private static string _suFix = " <-- @2@ ?";

    // ----------------------------------------- //
    private static ActionManager _actionManager = null;
    public static ActionManager Action {
        get {
            if (_actionManager != null)
                return _actionManager;

            _actionManager = GameObject.FindObjectOfType<ActionManager>();
            if (_actionManager != null)
                return _actionManager;

            // MUST NEED del PlayerInput para funcionar de forma correcta
            _actionManager = new GameObject(_preFix + "InputActions" + _suFix).AddComponent<ActionManager>();
            PlayerInput l_playerInput = _actionManager.GetComponent<PlayerInput>();
            l_playerInput.actions = Resources.Load<InputActionAsset>("Settings/InputActions");
            l_playerInput.currentActionMap = l_playerInput.actions.actionMaps[0];
            // \ MUST NEED \

            _actionManager.transform.SetParent(uCore.GameManager.transform);

            return _actionManager;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static AudioManager _audioManager = null;
    public static AudioManager Audio {
        get {
            if (_audioManager != null)
                return _audioManager;

            _audioManager = GameObject.FindObjectOfType<AudioManager>();
            if (_audioManager != null)
                return _audioManager;

            _audioManager = new GameObject(_preFix + "Audio" + _suFix).AddComponent<AudioManager>();
            _audioManager.transform.SetParent(uCore.GameManager.transform);

            return _audioManager;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static SceneDirector _sceneDirector = null;
    public static SceneDirector Director {
        get {
            if (_sceneDirector != null)
                return _sceneDirector;

            _sceneDirector = GameObject.FindObjectOfType<SceneDirector>();
            if (_sceneDirector != null)
                return _sceneDirector;

            _sceneDirector = new GameObject(_preFix + "Director" + _suFix).AddComponent<SceneDirector>();
            _sceneDirector.transform.SetParent(uCore.GameManager.transform);

            return _sceneDirector;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static ParticleInstancer _particleInstancer = null;
    public static ParticleInstancer Particles {
        get {
            if (_particleInstancer != null)
                return _particleInstancer;

            _particleInstancer = GameObject.FindObjectOfType<ParticleInstancer>();
            if (_particleInstancer != null)
                return _particleInstancer;

            _particleInstancer = new GameObject(_preFix + "Particles").AddComponent<ParticleInstancer>();
            _actionManager.transform.SetParent(uCore.GameManager.transform);

            return _particleInstancer;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static LocalizationManager _localizationManager = null;
    public static LocalizationManager Localization {
        get {
            if (_localizationManager != null)
                return _localizationManager;

            _localizationManager = GameObject.FindObjectOfType<LocalizationManager>();
            if (_localizationManager != null)
                return _localizationManager;

            _localizationManager = new GameObject(_preFix + "Localization").AddComponent<LocalizationManager>();
            _localizationManager.transform.SetParent(uCore.GameManager.transform);

            return _localizationManager;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static FadeFX m_Fader = null;
    public static FadeFX FadeFX {
        get {
            if (m_Fader != null)
                return m_Fader;

            m_Fader = GameObject.FindObjectOfType<FadeFX>();

            return m_Fader;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static GameManager _gameManager = null;
    public static GameManager GameManager {
        get {
            if (_gameManager != null)
                return _gameManager;

            _gameManager = GameObject.FindObjectOfType<GameManager>();
            if (_gameManager != null)
                return _gameManager;

            _gameManager = new GameObject(_preFix + "uCore").AddComponent<uCore>().gameObject.AddComponent<GameManager>();

            return _gameManager;
        }
    }
    // ----------------------------------------- //

    // Destruye posibles GameObjects de tipo "GameManager" en la escena si ya existe uno en "DontDestroyOnLoad"
    private void InstanceDestroyer() {
        GameManager[] instances = GameObject.FindObjectsOfType<GameManager>();
        int count = instances.Length;

        if (count >= 1) {
            for (var i = 1; i < instances.Length; i++)
                GameObject.Destroy(instances[i].gameObject);
            _gameManager = instances[0];
        }
    }

    // Unity Awake
    void Awake() {
        InstanceDestroyer();
        DontDestroyOnLoad(this.gameObject);
    }

}

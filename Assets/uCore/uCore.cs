using UnityEngine;
using UnityEngine.InputSystem;

public class uCore : MonoBehaviour {

    private static string _preFix = "** -> ? ?? ";
    private static string _suFix = " <-- @2@ ?";

    // ----------------------------------------- //
    private static ActionManager m_ActionManager = null;
    public static ActionManager Action {
        get {
            if (m_ActionManager != null)
                return m_ActionManager;

            m_ActionManager = GameObject.FindObjectOfType<ActionManager>();
            if (m_ActionManager != null)
                return m_ActionManager;

            // MUST NEED del PlayerInput para funcionar de forma correcta
            m_ActionManager = new GameObject(_preFix + "InputActions" + _suFix).AddComponent<ActionManager>();
            PlayerInput l_playerInput = m_ActionManager.GetComponent<PlayerInput>();
            l_playerInput.actions = Resources.Load<InputActionAsset>("Settings/InputActions");
            l_playerInput.currentActionMap = l_playerInput.actions.actionMaps[0];
            // \ MUST NEED \

            m_ActionManager.transform.SetParent(uCore.GameManager.transform);

            return m_ActionManager;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static AudioManager m_AudioManager = null;
    public static AudioManager Audio {
        get {
            if (m_AudioManager != null)
                return m_AudioManager;

            m_AudioManager = GameObject.FindObjectOfType<AudioManager>();
            if (m_AudioManager != null)
                return m_AudioManager;

            m_AudioManager = new GameObject(_preFix + "Audio" + _suFix).AddComponent<AudioManager>();
            m_AudioManager.transform.SetParent(uCore.GameManager.transform);

            return m_AudioManager;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static SceneDirector m_Scenedirector = null;
    public static SceneDirector Director {
        get {
            if (m_Scenedirector != null)
                return m_Scenedirector;

            m_Scenedirector = GameObject.FindObjectOfType<SceneDirector>();
            if (m_Scenedirector != null)
                return m_Scenedirector;

            m_Scenedirector = new GameObject(_preFix + "Director" + _suFix).AddComponent<SceneDirector>();
            m_Scenedirector.transform.SetParent(uCore.GameManager.transform);

            return m_Scenedirector;
        }
    }
    // ----------------------------------------- //

    // ----------------------------------------- //
    private static ParticleInstancer m_ParticleInstancer = null;
    public static ParticleInstancer Particles {
        get {
            if (m_ParticleInstancer != null)
                return m_ParticleInstancer;

            m_ParticleInstancer = GameObject.FindObjectOfType<ParticleInstancer>();
            if (m_ParticleInstancer != null)
                return m_ParticleInstancer;

            m_ParticleInstancer = new GameObject(_preFix + "Particles").AddComponent<ParticleInstancer>();
            m_ActionManager.transform.SetParent(uCore.GameManager.transform);

            return m_ParticleInstancer;
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
    private static GameManager m_GameManager = null;
    public static GameManager GameManager {
        get {
            if (m_GameManager != null)
                return m_GameManager;

            m_GameManager = GameObject.FindObjectOfType<GameManager>();
            if (m_GameManager != null)
                return m_GameManager;

            m_GameManager = new GameObject(_preFix + "uCore").AddComponent<uCore>().gameObject.AddComponent<GameManager>();

            return m_GameManager;
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
            m_GameManager = instances[0];
        }
    }

    // Unity Awake
    void Awake() {
        InstanceDestroyer();
        DontDestroyOnLoad(this.gameObject);
    }

}

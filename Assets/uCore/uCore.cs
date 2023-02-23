using UnityEngine;
using UnityEngine.InputSystem;

public class uCore : MonoBehaviour {

    // ----------------------------------------- //
    private static ActionManager m_ActionManager = null;
    public static ActionManager Action {
        get {
            if (m_ActionManager != null)
                return m_ActionManager;

            m_ActionManager = UnityEngine.GameObject.FindObjectOfType<ActionManager>();
            if (m_ActionManager != null)
                return m_ActionManager;

            // MUST NEED del PlayerInput para funcionar de forma correcta
            m_ActionManager = new UnityEngine.GameObject("[=== InputActions").AddComponent<ActionManager>();
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

            m_AudioManager = UnityEngine.GameObject.FindObjectOfType<AudioManager>();
            if (m_AudioManager != null)
                return m_AudioManager;

            m_AudioManager = new UnityEngine.GameObject("[=== Audio").AddComponent<AudioManager>();
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

            m_Scenedirector = UnityEngine.GameObject.FindObjectOfType<SceneDirector>();
            if (m_Scenedirector != null)
                return m_Scenedirector;

            m_Scenedirector = new UnityEngine.GameObject("[=== Director").AddComponent<SceneDirector>();
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

            m_ParticleInstancer = UnityEngine.GameObject.FindObjectOfType<ParticleInstancer>();
            if (m_ParticleInstancer != null)
                return m_ParticleInstancer;

            m_ParticleInstancer = new UnityEngine.GameObject("[=== Particles").AddComponent<ParticleInstancer>();
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

            m_Fader = UnityEngine.GameObject.FindObjectOfType<FadeFX>();

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

            m_GameManager = UnityEngine.GameObject.FindObjectOfType<GameManager>();
            if (m_GameManager != null)
                return m_GameManager;

            m_GameManager = new UnityEngine.GameObject("[=== uCore").AddComponent<uCore>().gameObject.AddComponent<GameManager>();

            return m_GameManager;
        }
    }
    // ----------------------------------------- //

    // Destruye posibles GameObjects de tipo "GameManager" en la escena si ya existe uno en "DontDestroyOnLoad"
    private void InstanceDestroyer() {
        GameManager[] instances = UnityEngine.GameObject.FindObjectsOfType<GameManager>();
        int count = instances.Length;

        if (count >= 1) {
            for (var i = 1; i < instances.Length; i++)
                UnityEngine.GameObject.Destroy(instances[i].gameObject);
            m_GameManager = instances[0];
        }
    }

    // Unity Awake
    void Awake() {
        InstanceDestroyer();
        DontDestroyOnLoad(this.gameObject);
    }

}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ActionManager : MonoBehaviour {

    // Enum que determina el timpo de Input D: (algo más "homemade")
    public enum INPUT_SCHEME {
        I_KEYBOARD, I_GAMEPAD
    }

    // Observer para saber caundo se cambia de Scheme a.k.a enchufo el mando je
    public static event Action<INPUT_SCHEME> OnChangeInput;

    // Componente New Input System
    [SerializeField]
    private PlayerInput m_Input;

    // Control del Scheme actual
    [SerializeField]
    private INPUT_SCHEME m_CurrentScheme;
    public INPUT_SCHEME Scheme() { return m_CurrentScheme; }
    public bool GamePad() { return (Scheme().Equals(INPUT_SCHEME.I_GAMEPAD)); }
    public bool Keyboard() { return (Scheme().Equals(INPUT_SCHEME.I_KEYBOARD)); }

    // Actions
    private bool m_MoveRight;
    private bool m_MoveLeft;
    private bool m_MoveForward;
    private bool M_MoveBackward;
    private bool m_Jump;
    private bool m_Dash;
    private bool m_Run;
    private Vector2 m_CameraMovement;
    private bool m_Punch;
    private Vector2 m_Destination;
    private bool m_LClick;
    private bool m_Enter;
    private bool m_Escape;

    // Debug
    private bool m_KeyE;
    private bool m_KeyF;
    private bool m_KeyO;

    // Métodos Check??
    public bool MoveRight() { return m_MoveRight; }
    public bool MoveLeft() { return m_MoveLeft; }
    public bool MoveForward() { return m_MoveForward; }
    public bool MoveBackward() { return M_MoveBackward; }
    public bool KeyE() { return m_KeyE; }
    public bool KeyF() { return m_KeyF; }
    public bool KeyO() { return m_KeyO; }
    public bool Jump() { return m_Jump; }
    public bool Dash() { return m_Dash; }
    public bool Run() { return m_Run; }
    public Vector2 CameraMovement() { return m_CameraMovement; }
    public bool Punch() { return m_Punch; }
    public Vector2 Destination() { return m_Destination; }
    public bool LClick() { return m_LClick; }
    public bool Enter() { return m_Enter; }
    public bool Escape() { return m_Escape; }

    // Unity Awake
    void Awake() {
        m_Input = GetComponent<PlayerInput>();
        m_CurrentScheme = INPUT_SCHEME.I_KEYBOARD;
    }

    // Unity Start
    void Start() { }

    // Unity Update
    void Update() { }

    // * ----------------------------------------------------------------------------------------------------------------------------------- *
    // | - Send Mesagges - PlayerInput Component ------------------------------------------------------------------------------------------- |
    // V ----------------------------------------------------------------------------------------------------------------------------------- V
    void OnControlsChanged() {
        if (m_Input.currentControlScheme.Equals("Gamepad")) m_CurrentScheme = INPUT_SCHEME.I_GAMEPAD;
        if (m_Input.currentControlScheme.Equals("Keyboard&Mouse")) m_CurrentScheme = INPUT_SCHEME.I_KEYBOARD;
        OnChangeInput?.Invoke(Scheme());
    }

    void OnMoveRight(InputValue value) {
        m_MoveRight = value.isPressed;
    }

    void OnMoveLeft(InputValue value) {
        m_MoveLeft = value.isPressed;
    }

    void OnMoveForward(InputValue value) {
        m_MoveForward = value.isPressed;
    }

    void OnMoveBackward(InputValue value) {
        M_MoveBackward = value.isPressed;
    }

    void OnKeyE(InputValue value) {
        m_KeyE = value.isPressed;
    }

    void OnKeyF(InputValue value) {
        m_KeyF = value.isPressed;
    }

    IEnumerator OnKeyO(InputValue value) {
        m_KeyO = value.isPressed;
        yield return new WaitForEndOfFrame();
        m_KeyO = false;
    }

    IEnumerator OnJump(InputValue value) {
        m_Jump = value.isPressed;
        yield return new WaitForEndOfFrame();
        m_Jump = false;
    }

    IEnumerator OnDash(InputValue value) {
        m_Dash = value.isPressed;
        yield return new WaitForEndOfFrame();
        m_Dash = false;
    }

    void OnRun(InputValue value) {
        m_Run = value.isPressed;
    }

    void OnCameraMovement(InputValue value) {
        m_CameraMovement = value.Get<Vector2>();
        m_CameraMovement.Normalize();
    }

    IEnumerator OnPunch(InputValue value) {
        m_Punch = value.isPressed;
        yield return new WaitForEndOfFrame();
        m_Punch = false;
    }

    void OnDestination(InputValue value) {
        m_Destination = value.Get<Vector2>();
    }

    IEnumerator OnClick(InputValue value) {
        m_LClick = value.isPressed;
        yield return new WaitForEndOfFrame();
        m_LClick = false;
    }

    IEnumerator OnEnter(InputValue value) {
        m_Enter = value.isPressed;
        yield return new WaitForEndOfFrame();
        m_Enter = false;
    }

    IEnumerator OnEscape(InputValue value) {
        m_Escape = value.isPressed;
        yield return new WaitForEndOfFrame();
        m_Escape = false;
    }
    // A ----------------------------------------------------------------------------------------------------------------------------------- A

}

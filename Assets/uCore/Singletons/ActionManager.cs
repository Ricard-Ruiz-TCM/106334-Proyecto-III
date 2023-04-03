using System;
using System.Collections;
using System.Collections.Generic;
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
    private PlayerInput _input;

    // Control del Scheme actual
    [SerializeField]
    private INPUT_SCHEME _currentScheme;
    public INPUT_SCHEME Scheme() { return _currentScheme; }
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

    // Métodos Check??
    public bool MoveRight() { return m_MoveRight; }
    public bool MoveLeft() { return m_MoveLeft; }
    public bool MoveForward() { return m_MoveForward; }
    public bool MoveBackward() { return M_MoveBackward; }
    public bool Jump() { return m_Jump; }
    public bool Dash() { return m_Dash; }
    public bool Run() { return m_Run; }
    public Vector2 CameraMovement() { return m_CameraMovement; }
    public bool Punch() { return m_Punch; }
    public Vector2 Destination() { return m_Destination; }
    public bool LClick() { return m_LClick; }

    // InputActions for all Unity KeyCodes
    private Dictionary<KeyCode, InputAction> _keyActions;

    // Unity Awake
    void Awake() {
        _input = GetComponent<PlayerInput>();
        _currentScheme = INPUT_SCHEME.I_KEYBOARD;
        _keyActions = new Dictionary<KeyCode, InputAction>();
    }

    // Unity Start
    void Start() { }

    // Unity Update
    void Update() { }

    // * ----------------------------------------------------------------------------------------------------------------------------------- *
    // | - Send Mesagges - PlayerInput Component ------------------------------------------------------------------------------------------- |
    // V ----------------------------------------------------------------------------------------------------------------------------------- V
    void OnControlsChanged() {
        if (_input.currentControlScheme.Equals("Gamepad")) _currentScheme = INPUT_SCHEME.I_GAMEPAD;
        if (_input.currentControlScheme.Equals("Keyboard&Mouse")) _currentScheme = INPUT_SCHEME.I_KEYBOARD;
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

    // A ----------------------------------------------------------------------------------------------------------------------------------- A

    // * ----------------------------------------------------------------------------------------------------------------------------------- *
    // | - KeyCodes InputActions ----------------------------------------------------------------------------------------------------------- |
    // V ----------------------------------------------------------------------------------------------------------------------------------- V

    public void AddNewKey(KeyCode key) {
        if (!_keyActions.ContainsKey(key)) {
            InputAction action = new InputAction("key" + key.ToString(), InputActionType.Button, binding: "<Keyboard>/" + key.ToString().ToLower());
            _keyActions.Add(key, action);
            action.Enable();
        }
    }

    public bool GetKey(KeyCode key) {
        AddNewKey(key);
        return _keyActions[key].IsPressed();
    }
    public bool GetKeyDown(KeyCode key) {
        AddNewKey(key);
        return _keyActions[key].WasPressedThisFrame();
    }
    public bool GetKeyUp(KeyCode key) {
        AddNewKey(key);
        return _keyActions[key].WasReleasedThisFrame();
    }

    // A ----------------------------------------------------------------------------------------------------------------------------------- A

}

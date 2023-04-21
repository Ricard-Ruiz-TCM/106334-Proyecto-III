using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(PlayerInput))]
public class ActionManager : MonoBehaviour {

    // Observer para saber caundo se cambia de Scheme a.k.a enchufo/uso el mando eje
    public static event Action<inputScheme> OnChangeInput;

    [SerializeField, Header("Player Input:")]
    private PlayerInput _input;

    [SerializeField, Header("Current Scheme:")]
    private inputScheme _currentScheme;
    public inputScheme Scheme() {
        return _currentScheme;
    }
    public bool GamePad() {
        return (Scheme().Equals(inputScheme.GamePad));
    }
    public bool Keyboard() {
        return (Scheme().Equals(inputScheme.Keyboard));
    }

    // InputActions for all Unity KeyCodes
    private Dictionary<KeyCode, InputAction> _keyActions;

    // Determine if the input system is ready to use
    private bool _isConfigured = false;
    public bool isInputConfigured() {
        return _isConfigured;
    }

    // Unity OnEnable
    void OnEnable() {
        InputSystem.onAnyButtonPress.CallOnce(OnAnyButtonPress);
    }

    // Unity Awake
    void Awake() {
        _input = GetComponent<PlayerInput>();
        _currentScheme = inputScheme.Keyboard;
        _keyActions = new Dictionary<KeyCode, InputAction>();
    }

    // * ----------------------------------------- *
    // | - Send Mesagges - PlayerInput Component - |
    // V ----------------------------------------- V
    void OnControlsChanged() {
        if (_input.currentControlScheme.Equals("Gamepad"))
            _currentScheme = inputScheme.GamePad;
        if (_input.currentControlScheme.Equals("Keyboard&Mouse"))
            _currentScheme = inputScheme.Keyboard;
        OnChangeInput?.Invoke(Scheme());
    }
    public bool onClick;
    void OnClick(InputValue value) {

    }

    // A ----------------------------------------- A

    /** Método OnAnyButtonPress
     * Se encarga de configurar el input, setenado el esquema
     * Hace el call de InputSystem.onAnyButtonPress */
    void OnAnyButtonPress(InputControl inputControl) {
        if (!Enum.TryParse(inputControl.device.description.deviceClass, out _currentScheme)) {
            Debug.LogWarning("ActionManager -> Device nos supported: " + inputControl.device.description.deviceClass);
        } else {
            _isConfigured = true;
        }
        OnChangeInput?.Invoke(Scheme());
    }

    // * ------------------------- *
    // | - KeyCodes InputActions - |
    // V ------------------------- V
    public void AddNewKey(KeyCode key) {
        if (!_keyActions.ContainsKey(key)) {
            InputAction action = new InputAction("key" + key.ToString(), InputActionType.Button, binding: "<Keyboard>/" + key.ToString().ToLower());
            _keyActions.Add(key, action);
            action.Enable();
        }
    }
    // Key, KeyDown & KeyUp
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
    // A ------------------------- A

}

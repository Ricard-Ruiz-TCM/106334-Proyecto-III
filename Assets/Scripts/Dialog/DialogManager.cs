using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    /** Singleton Instance */
    public static DialogManager instance = null;

    /** Callbacks */
    public Action onEndDialog;
    public Action<DialogNode> onNextDialog;

    /** Current Node */
    [SerializeField, Header("Dialogs:")]
    private DialogNode _current = null;
    [SerializeField]
    private List<DialogNode> _history = new List<DialogNode>();

    /** Special Nodes */
    private DialogNode _lastOptionsNode;
    public DialogNode LastOptions() { return _lastOptionsNode; }

    private DialogNode _lastTriggerNode;
    public DialogNode LastTrigger() { return _lastTriggerNode; }

    // Unity Awake
    void Awake() {
        // Singleton
        if ((instance != null) && (instance != this)) {
            GameObject.Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    /** Inicia el dialogo desde un nodo concreto */
    public void StartDialog(DialogNode node) {
        _current = node;
        onNextDialog?.Invoke(_current);
    }

    /** Control del siguietne dialogo */
    public void NextDialog() {
        NextDialog(_current.nextNode);
    }

    /** Control del siguietne dialogo, pero especificando cual ;3 */
    public void NextDialog(DialogNode node) {
        onEndDialog?.Invoke();

        // Exit si vamos null
        if (node == null)
            return;

        _history.Add(_current);
        _current = node;


        // NextDialog
        onNextDialog?.Invoke(_current);

        // Trigger DialogNode
        if (_current is DialogTrigger) {
            _lastTriggerNode = _current;
            ((DialogTrigger)_current).Trigger();
            NextDialog(_current.nextNode);
        } 
        // Options DialogNode
        else if (_current.options.Count != 0) {
            _lastOptionsNode = _current;
        }
    }

}

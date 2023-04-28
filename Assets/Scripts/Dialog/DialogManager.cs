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
        // Add to pasts
        _history.Add(_current);
        _current = node;

        // Trigger DialogNode
        if (_current is DialogTrigger) {
            ((DialogTrigger)_current).Trigger();
            NextDialog(_current.nextNode);
        }

        // NextDialog
        onNextDialog?.Invoke(_current);
    }

}

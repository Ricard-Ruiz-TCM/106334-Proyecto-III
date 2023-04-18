using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    // Current Dialogue Node
    [SerializeField, Header("Dialogue Node:")]
    private DialogueNode _current;

    [SerializeField, Header("Player:")]
    private Image _playerImage;
    [SerializeField]
    private GameObject _optionsUI;

    [SerializeField, Header("Speaker:")]
    private GameObject _speakerUI;
    [SerializeField]
    private Image _speakerImage;
    [SerializeField]
    private UIText _speakerNameText;
    [SerializeField]
    private UIText _speakerMessageText;

    public void StartDialogue(DialogueNode node) {
        NextDialogue(node);
    }

    private void NextDialogue(DialogueNode node) {
        _current = node;
        UpdateDialogue();       
    }

    public void UpdateDialogue() {
        Clear();
       if (_current.EndNode) {

        }
    }

    private void Clear() { 
        
    }

}
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static event Action onEndDialogue; 

    [SerializeField, Header("Dialogue Node:")]
    private DialogueNode _current;

    [SerializeField, Header("Dialogue Option:")]
    private GameObject _optionPrefab;

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

    // Control
    private bool _displayingText = false;
    [SerializeField, Header("Timing:")]
    private float _textSpeed = 0.05f;

    // Inicia un dialoog
    public void StartDialogue(DialogueNode node) {
        gameObject.SetActive(true);
        NextDialogue(node);
    }

    // Set del próximo dialogo
    private void NextDialogue(DialogueNode node) {
        _current = node;
        UpdateDialogue();
    }

    // Limpia y actualiza el flow de dialogos
    // También se encarga de la interfaz
    public void UpdateDialogue() {
        Clear();

        // Desactivamos dialogo
        _speakerUI.SetActive(false);
        _optionsUI.SetActive(false);

        // Last Dialogue Node
        if (_current.EndNode) {
            gameObject.SetActive(false);
            onEndDialogue?.Invoke();
            return;
        }

        // Trigger Node
        if (_current.Trigger) {
            _current._trigger.Trigger();
        }

        // Node with Options
        if (_current.Options) {
            SetAlpha(_speakerImage, 0.5f);
            _optionsUI.SetActive(true);
            foreach (DialogueOption op in _current._options) {
                GameObject option = GameObject.Instantiate(_optionPrefab, _optionsUI.transform);
                option.GetComponentInChildren<UIText>().SetKey(op._textKey);
                option.GetComponent<Button>().onClick.AddListener(() => {
                    NextDialogue(op._next);
                });
            }
            return;
        }

        // Normal Node
        _displayingText = true;
        _speakerUI.SetActive(true);
        SetAlpha(_speakerImage, 1f);
        SetAlpha(_playerImage, 0.5f);
        _speakerNameText.SetKey(_current._textKeyName);
        StartCoroutine(C_DisplayText(uCore.Localization.GetText(_current._textKeyMessage)));
    }

    // Coroutine para mostrar el texto timeado por _speed
    private IEnumerator C_DisplayText(string text, int pos = 0) {
        if (pos == text.Length) {
            _displayingText = false;
        } else {
            _speakerMessageText.UpdateText(text.Substring(0, pos + 1));
            yield return new WaitForSeconds(_textSpeed);
            StartCoroutine(C_DisplayText(text, pos + 1));
        }
    }

    // Set del alpha para las imagenes
    private void SetAlpha(Image img, float value) {
        img.color = new Color(1f, 1f, 1f, value);
    }

    // Limpia todos los elementos del dialogo y borra las opciones
    private void Clear() {
        _speakerNameText.Clear();
        _speakerMessageText.Clear();
        foreach (Transform child in _optionsUI.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Clickar en el texto del dialogo
    public void EVENT_OnClickDialogue() {
        if (_displayingText) {
            StopAllCoroutines();
            _speakerMessageText.SetKey(_current._textKeyMessage);
            _displayingText = false;
        } else {
            NextDialogue(_current._next);
        }
    }

}
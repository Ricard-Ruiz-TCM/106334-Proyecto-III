using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour {

    private DialogNode _currentDialog;

    /** Option Prefab */
    private GameObject _optionPrefab = null;

    [SerializeField, Header("Speakers:")]
    private Image _speaker;

    [SerializeField, Header("Options:")]
    private GameObject _panelOptions;

    [SerializeField, Header("Texts:")]
    private GameObject _panelText;
    [SerializeField]
    private UIText _txtSpeakerName;
    [SerializeField]
    private UIText _txtSpeakerText;

    // Control
    //[SerializeField, Header("Turn:")] // ready -> Turno comenzado, "entando" al turno | doing -> Haciendo el turno | done -> Turno acabado, "cambiando" de turno
    private progress _dialogProgress = progress.ready;

    [SerializeField, Header("Timing:")]
    private float _textSpeed = 0.05f;

    // Unity OnEnable
    void OnEnable() {
        DialogManager.instance.onEndDialog += Clear;
        DialogManager.instance.onNextDialog += UpdateDialog;
    }

    // Unity OnDisable
    void OnDisable() {
        DialogManager.instance.onEndDialog += Clear;
        DialogManager.instance.onNextDialog += UpdateDialog;
    }

    // Unity Awake
    void Awake() {
        _optionPrefab = Resources.Load<GameObject>("Prefabs/Interface/OptionUI");
    }

    // Limpia y actualiza el flow de dialogos
    // También se encarga de la interfaz
    public void UpdateDialog(DialogNode current) {

        _currentDialog = current;

        // Desactivamos dialogo
        _panelText.SetActive(false);
        _panelOptions.SetActive(false);

        // Last Dialogue Node
        if (_currentDialog == null) {
            gameObject.SetActive(false);
            return;
        }

        // Node with Options
        if (_currentDialog.options.Count != 0) {
            SetAlpha(_speaker, 0f);
            _panelOptions.SetActive(true);
            foreach (DialogOption op in _currentDialog.options) {
                GameObject option = GameObject.Instantiate(_optionPrefab, _panelOptions.transform);
                option.GetComponentInChildren<UIText>().SetKey(op.keyText);
                option.GetComponent<Button>().onClick.AddListener(() => {
                    FMODManager.instance.PlayOneShot(FMODEvents.instance.PressButtonUI);
                    DialogManager.instance.NextDialog(op.next);
                });
            }
            return;
        }

        // Normal Node
        _dialogProgress = progress.doing;
        _panelText.SetActive(true);
        SetAlpha(_speaker, 1f);
        _speaker.sprite = _currentDialog.speaker;
        _txtSpeakerName.SetKey(_currentDialog.keyName);
        try
        {
            StartCoroutine(C_DisplayText(uCore.Localization.GetText(_currentDialog.keyMessage)));
        } catch(Exception ex) { }
    }


    // Coroutine para mostrar el texto timeado por _speed
    private IEnumerator C_DisplayText(string text, int pos = 0) {
        if (pos == text.Length) {
            _dialogProgress = progress.done;
        } else {
            _txtSpeakerText.UpdateText(text.Substring(0, pos + 1));
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
        _txtSpeakerName.Clear();
        _txtSpeakerText.Clear();
        foreach (Transform child in _panelOptions.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Clickar en el texto del dialogo
    public void EVENT_OnClickDialogue() {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.PressButtonUI);
        if (_dialogProgress.Equals(progress.doing)) {
            StopAllCoroutines();
            _txtSpeakerText.SetKey(_currentDialog.keyMessage);
            _dialogProgress = progress.done;
        } else {
            DialogManager.instance.NextDialog();
        }
    }

}


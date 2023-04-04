using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIText : MonoBehaviour {

    // TextKey
    private string _textKey = "";

    // Text
    private TextMeshProUGUI _text {
        get {
            if (_text == null) 
                _text = GetComponent<TextMeshProUGUI>();

            return _text;
        }
        set { _text = value; }
    }

    // Unity OnEnable
    private void OnEnable() {
        LocalizationManager.OnChangeLocalization += UpdateText;
    }

    // Unity OnDisable
    private void OnDisable() {
        LocalizationManager.OnChangeLocalization -= UpdateText;
    }

    // Unity Awake
    void Awake() {
        _textKey = _text.text;
    }

    // Unity Start
    void Start() {
        UpdateText();
    }

    // Métodos para actualizar el valor del dependiendo del tipo de parametro
    public void UpdateText(int str) { SetText(str); }
    public void UpdateText(float str) { SetText(str); }
    public void UpdateText(double str) { SetText(str); }
    public void UpdateText(bool str) { SetText(str); }
    public void UpdateText(string str) { SetText(str); }
    public void UpdateText(short str) { SetText(str); }
    // Método oconcreto para setear el texto según Localization
    public void UpdateText() { SetText(uCore.Localization.GetText(_textKey)); }

    // Establece el valor
    private void SetText(IConvertible str) {
        _text.text = str.ToString();
    }

    // Método para limpiar el texto
    public void Clear() {
        _text.text = "";
    }

}

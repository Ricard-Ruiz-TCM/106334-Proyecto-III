using System;
using TMPro;
using UnityEngine;

/** class UIText
 * -------------
 * 
 * Clase para controlar los textos de TMPro
 * funcionand con el sistema de Localization.
 * 
 * @see LocalizationManager
 * @see language
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v2.0 (04/2023)
 * 
 */

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIText : MonoBehaviour {

    /** TextKey a.k.a. ID del json */
    private string _textKey = "";
    private TextMeshProUGUI _text {
        get {
            return GetComponent<TextMeshProUGUI>();
        }
        set {

        }
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
    public void UpdateText(int str) {
        SetText(str);
    }
    public void UpdateText(float str) {
        SetText(str);
    }
    public void UpdateText(double str) {
        SetText(str);
    }
    public void UpdateText(bool str) {
        SetText(str);
    }
    public void UpdateText(string str) {
        SetText(str);
    }
    public void UpdateText(short str) {
        SetText(str);
    }
    // Método concreto para setear el texto según Localization
    public void UpdateText() {
        if (uCore.Localization.Exists(_textKey)) {
            SetText(uCore.Localization.GetText(_textKey));
        }
    }

    /** Método SetKey
     * Establece la key dle texto y acutaliza
     * @param string key Key del json*/
    public void SetKey(string key) {
        _textKey = key;
        UpdateText();
    }

    /** Método GetText
     * Devuelve el texto que tiene el TMPro
     * @return string Texto */
    public string GetText() {
        return _text.text;
    }

    /** Método AddText(string text)
     * Añade texto al texto ya existente
     * @param string text Texto para añadir */
    public void AddText(string text) {
        _text.text += text;
    }

    /** Método SetText
     * Establece el valor del texto
     * @param IConvertible str Objeto que puede convertirse y llamar al método "ToString()" */
    private void SetText(IConvertible str) {
        _text.text = str.ToString();
    }

    /** Método Clear */
    public void Clear() {
        _text.text = "";
    }

}

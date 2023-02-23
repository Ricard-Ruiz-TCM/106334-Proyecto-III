using System;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

    [SerializeField]
    private Text m_Text;

    // M�todos para actualizar el valor de m_Text.text dependiendo del tipo de parametro
    public void UpdateText(int str) { SetText(str); }
    public void UpdateText(float str) { SetText(str); }
    public void UpdateText(double str) { SetText(str); }
    public void UpdateText(bool str) { SetText(str); }
    public void UpdateText(string str) { SetText(str); }
    public void UpdateText(short str) { SetText(str); }

    // Esttablec� el valor
    private void SetText(IConvertible str) {
        m_Text.text = str.ToString();
    }

    // M�todo para limpiar el texto
    public void Clear() {
        m_Text.text = "";
    }

}

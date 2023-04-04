using UnityEngine;
using UnityEngine.UI;

public class UIImage : MonoBehaviour {

    // Fill Amount
    private float m_value;

    [SerializeField]
    private Image m_Image;

    // M�todo para llenar la barra, o full o valores X
    // In: value -> Nuevo valor
    // In: maxValue -> Valor m�ximo posible de value
    public void Fill(float value = 1f, float maxValue = 1f) {
        m_value = value / maxValue;
        m_Image.fillAmount = m_value;
    }

    // M�todo para vaciar la barra
    public void Clear() {
        Fill(0f);
    }

}

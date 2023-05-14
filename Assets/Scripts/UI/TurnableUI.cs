using UnityEngine;
using UnityEngine.UI;

class TurnableUI : MonoBehaviour {

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private UIText _text;
    [SerializeField]
    private Slider _slider;

    public void SetTurnable(Actor turnable) {
        _text.UpdateText(turnable.gameObject.name);
        if (_slider != null)
            _slider.value = ((float)turnable.GetHealth() / (float)turnable.MaxHealth());


    }

}

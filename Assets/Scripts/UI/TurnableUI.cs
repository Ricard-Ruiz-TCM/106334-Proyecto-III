using UnityEngine;
using UnityEngine.UI;

class TurnableUI : MonoBehaviour {

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private UIText _text;
    [SerializeField]
    private Image _filler;

    public void SetTurnable(Turnable turnable) {
        _text.UpdateText(turnable.gameObject.name);
        if (_filler != null) {
            _filler.fillAmount = (float)((float)((BasicActor)turnable).health() / (float)((BasicActor)turnable).maxHealth());
        }
        _icon.sprite = turnable.headIcon;
    }

}

using UnityEngine;
using UnityEngine.UI;

class TurnableUI : MonoBehaviour {

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private UIText _text;

    public void SetTurnable(Actor turnable) {
        _text.UpdateText(turnable.gameObject.name);
    }

}

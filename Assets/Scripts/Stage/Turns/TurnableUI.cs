using UnityEngine;
using UnityEngine.UI;

class TurnableUI : MonoBehaviour {

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private UIText _text;
    [SerializeField]
    private Slider _slider;

    public void SetTurnable(ITurnable turnable) {
        
    }

}

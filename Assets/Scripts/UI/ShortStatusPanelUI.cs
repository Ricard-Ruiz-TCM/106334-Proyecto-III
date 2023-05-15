using UnityEngine;
using UnityEngine.UI;

public class ShortStatusPanelUI : MonoBehaviour {

    public Image _icon;
    public UIText _duration;

    public void UpdateStatus(BuffItem si) {
        _icon.sprite = si.buff.icon;
        _duration.UpdateText(si.duration);
    }

}

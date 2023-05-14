using UnityEngine;
using UnityEngine.UI;

public class LongStatusPanelUI : MonoBehaviour {
    
    public Image _icon;
    public UIText _name;
    public UIText _duration;

    public void UpdateStatus(StatusItem si) {
        _icon.sprite = si.status.icon;
        _name.SetKey(si.status.keyName);
        _duration.UpdateText(si.duration);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class LongStatusPanelUI : MonoBehaviour {
    
    public Image _icon;
    public UIText _name;
    public UIText _duration;

    public void UpdateStatus(BuffItem si) {
        _icon.sprite = si.buff.icon;
        _name.SetKey(si.buff.keyName);
        _duration.UpdateText(si.duration);
    }

}

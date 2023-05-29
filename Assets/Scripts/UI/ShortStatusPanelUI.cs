using UnityEngine;
using UnityEngine.UI;

public class ShortStatusPanelUI : MonoBehaviour {

    private BuffItem _buffItem;
    public BuffItem bfItem => _buffItem;

    public Image _icon;
    public UIText _duration;

    public void UpdateStatus(BuffItem bi) {
        _buffItem = bi;
        _icon.sprite = bi.buff.icon;
        _duration.UpdateText(bi.duration);
    }

}

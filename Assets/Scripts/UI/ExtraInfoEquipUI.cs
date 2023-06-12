using UnityEngine;
using UnityEngine.UI;

public class ExtraInfoEquipUI : MonoBehaviour {

    public Sprite _baseSprite;

    public Image _icon;
    public UIText _name;
    public UIText _value;

    public void setItem(Sprite icon, string keyname, int value, bool armor = true) {

        _icon.sprite = icon;
        _name.SetKey(keyname);
        _value.UpdateText((armor ? "Arm:" : "Dmg:") + " " + value.ToString());

    }

    public void clear() {
        _icon.sprite = _baseSprite;
        _name.UpdateText("");
        _value.UpdateText("");
    }
}
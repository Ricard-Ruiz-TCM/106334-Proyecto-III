using UnityEngine;

public class ExtraInfoPanelUI : MonoBehaviour {

    public UIText _name, _desc;

    public void UpdateText(string name, string desc) {
        _name.SetKey(name);
        _desc.SetKey(desc);
    }
}

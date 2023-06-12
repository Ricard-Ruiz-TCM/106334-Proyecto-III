using UnityEngine.EventSystems;

public class ExtraInfoHoverActivator : HoverActivator {

    /** Skill Button */
    private ExtraInfoPanelUI _PANEL;

    public string _name, _desc;

    // Unity Awake
    void Awake() {
        _PANEL = GetComponent<ExtraInfoPanelUI>();
    }


    public void setKeys(string n, string d) {
        _name = n; _desc = d;
    }

    // OnPointerEnter
    public override void OnPointerEnter(PointerEventData eventData) {
        if (_name == "")
            return;

        base.OnPointerEnter(eventData);
        _object.GetComponent<ExtraInfoPanelUI>().UpdateText(_name, _desc);
    }

}


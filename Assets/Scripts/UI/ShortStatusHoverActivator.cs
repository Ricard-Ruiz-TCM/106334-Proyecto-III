using UnityEngine.EventSystems;

public class ShortStatusHoverActivator : HoverActivator {

    /** Skill Button */
    private ShortStatusPanelUI _buffButtonUI;

    // Unity Awake
    void Awake() {
        _buffButtonUI = GetComponent<ShortStatusPanelUI>();
    }

    // OnPointerEnter
    public override void OnPointerEnter(PointerEventData eventData) {
        base.OnPointerEnter(eventData);
        _object.GetComponent<ShortStatusInfoUI>().Set(_buffButtonUI.bfItem);
    }

}

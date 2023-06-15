using UnityEngine.EventSystems;

public class SkillHoverActivator : HoverActivator {

    /** Skill Button */
    public SkillButtonUI _skillButtonUI;

    // Unity Awake
    void Awake() {
        _skillButtonUI = GetComponent<SkillButtonUI>();
    }

    // OnPointerEnter
    public override void OnPointerEnter(PointerEventData eventData) {
        base.OnPointerEnter(eventData);
        _object.GetComponent<SkillButtonInfoUI>().Set(_skillButtonUI.SkItem);
    }

}

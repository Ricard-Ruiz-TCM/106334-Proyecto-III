using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField, Header("Info Panel:")]
    private SkillButtonInfoUI _panelInfo;

    /** Skill Button */
    private SkillButtonUI _skillButtonUI;

    // Unity Awake
    void Awake() {
        _skillButtonUI = GetComponent<SkillButtonUI>();
    }

    // OnPointerEnter
    public void OnPointerEnter(PointerEventData eventData) {
        _panelInfo.gameObject.SetActive(true);
        _panelInfo.Set(_skillButtonUI.SkItem);
    }

    // OnPointerExit
    public void OnPointerExit(PointerEventData eventData) {
        _panelInfo.gameObject.SetActive(false);
    }
}

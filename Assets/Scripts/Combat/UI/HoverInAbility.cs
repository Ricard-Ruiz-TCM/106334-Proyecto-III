using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private GameObject abilityExplanation;

    public void OnPointerEnter(PointerEventData eventData) {
        abilityExplanation.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        abilityExplanation.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() {
        abilityExplanation = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {

    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField, Header("GameObject")]
    protected GameObject _object;

    public virtual void OnPointerEnter(PointerEventData eventData) {
        _object.SetActive(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        _object.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;


public class HoverActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField, Header("GameObject")]
    public GameObject _object;

    public virtual void OnPointerEnter(PointerEventData eventData) {
        if (_object != null)
            _object.SetActive(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        if (_object != null)
            _object.SetActive(false);
    }
}


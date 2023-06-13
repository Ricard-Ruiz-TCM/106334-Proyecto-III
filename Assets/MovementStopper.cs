using UnityEngine;
using UnityEngine.EventSystems;

public class MovementStopper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData) {
        ((ManualActor)uCore.GameManager.getPlayer()).disableMovement();
    }

    public void OnPointerExit(PointerEventData eventData) {
        ((ManualActor)uCore.GameManager.getPlayer()).enableMovement();
    }
}

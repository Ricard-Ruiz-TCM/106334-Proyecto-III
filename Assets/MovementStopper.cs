using UnityEngine;
using UnityEngine.EventSystems;

public class MovementStopper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData) {
        Stage.StageBuilder.clearGrid();
        Stage.StageBuilder.clearPath();
        ((ManualActor)uCore.GameManager.getPlayer()).disableMovement();
    }

    public void OnPointerExit(PointerEventData eventData) {
        Stage.StageBuilder.clearGrid();
        Stage.StageBuilder.clearPath();
        ((ManualActor)uCore.GameManager.getPlayer()).enableMovement();
    }
}

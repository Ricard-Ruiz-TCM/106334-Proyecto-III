using System.Collections.Generic;
using UnityEngine;

class TurnManagerUI : MonoBehaviour {

    [SerializeField, Header("Prefabs")]
    private GameObject _bTurnableUI;
    [SerializeField]
    private GameObject _sTurnableUI;

    // Unity Start
    void Start() {
        TurnManager.instance.onModifyList += UpdateTurnList;
        TurnManager.instance.onEndTurn += UpdateTurnList;
    }

    /** Añade los elementos a los turnos */
    public void UpdateTurnList() {
        ClearList();

        List<Actor> turnables = TurnManager.instance.Sorted();
        // Instant the bigOne
        if (turnables.Count > 1) {
            InstantiateUI(_bTurnableUI).SetTurnable(turnables[0]);
            for (int i = 1; i < turnables.Count; i++) {
                // Instant the smallOnes
                InstantiateUI(_sTurnableUI).SetTurnable(turnables[i]);
            }
        }
    }

    private TurnableUI InstantiateUI(GameObject prefab) {
        return GameObject.Instantiate(prefab, transform).GetComponent<TurnableUI>();
    }

    public void ClearList() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void BTN_EndPositioning() {
        TurnManager.instance.positioningDone();
    }

}

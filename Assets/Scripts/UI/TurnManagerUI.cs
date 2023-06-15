using System.Collections.Generic;
using UnityEngine;

class TurnManagerUI : MonoBehaviour {

    [SerializeField, Header("Prefabs")]
    private GameObject _bTurnableUI;
    [SerializeField]
    private GameObject _sTurnableUI;

    [SerializeField]
    private TargetInfoUI _turnInfo;

    // Unity OnEnable
    void OnEnable() {
        TurnManager.onStartTurn += UpdateTurnList;
        TurnManager.onModifyAttenders += UpdateTurnList;
    }

    // Unity OnDisable
    void OnDisable() {
        TurnManager.onStartTurn -= UpdateTurnList;
        TurnManager.onModifyAttenders -= UpdateTurnList;
    }

    /** Añade los elementos a los turnos */
    public void UpdateTurnList() {
        ClearList();

        List<Turnable> turnables = new List<Turnable>(TurnManager.instance.attenders.ToArray());
        turnables.RemoveAll(x => x is StaticActor);
        for (int i = 0; i < turnables.Count; i++) {
            if (turnables[i].Equals(TurnManager.instance.current)) {
                InstantiateUI(_bTurnableUI).SetTurnable(turnables[i]);
            } else {
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
        TurnManager.instance.completeRoundType(roundType.positioning);
    }

}

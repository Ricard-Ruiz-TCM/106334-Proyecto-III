﻿using UnityEngine;

using System.Collections.Generic;

class TurnManagerUI : MonoBehaviour {

    [SerializeField, Header("Prefabs")]
    private GameObject _bTurnableUI;
    [SerializeField]
    private GameObject _sTurnableUI;

    private TurnManager _turnManager;

    // Unity Awake
    void Awake() {
        _turnManager = GameObject.FindObjectOfType<TurnManager>();
        _turnManager.onModifyTurnList += UpdateTurnList;
    }

    /** Añade los elementos a los turnos */
    public void UpdateTurnList() {
        ClearList();

       /* List<ITurnable> turnables = _turnManager.TurnablesSorted();
        // Instant the bigOne
        InstantiateUI(_bTurnableUI).SetTurnable(turnables[0]);
        for (int i = 1; i < turnables.Count; i++) {
            // Instant the smallOnes
            InstantiateUI(_sTurnableUI).SetTurnable(turnables[i]);
        }
       */
    }

    private TurnableUI InstantiateUI(GameObject prefab) {
        return GameObject.Instantiate(prefab, transform).GetComponent<TurnableUI>();
    }

    public void ClearList() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    // Callback cuando modificando la lista
    public Action onModifyTurnList;

    [SerializeField]
    private int _turnIndex = 0;
    [SerializeField]
    private List<ITurnable> _turnables;

    [SerializeField]
    private float _turnDelay = 2f;

    private bool _waiting = false;

    private void Awake() {
        _turnables = new List<ITurnable>();
    }

    public void Add(ITurnable element) {
        if (!Contains(element))
            _turnables.Add(element);

        onModifyTurnList?.Invoke();
    }

    public void Remove(ITurnable element) {
        if (Contains(element))
            _turnables.Remove(element);

        onModifyTurnList?.Invoke();
    }

    // Unity Update
    private void Update() {
        ITurnable turneable = _turnables[_turnIndex];

        // Estamos esperando que el turno cambie
        if (_waiting)
            return;

        // Turno finalizado
        if (turneable.hasTurnEnded) {
            StartCoroutine(NextTurn());
            turneable.BeginTurn();
            return;
        }

        if (turneable.CanMove()) {
            turneable.Move();
        }

        if (turneable.CanAct()) {
            turneable.Act();
        }

        if ((turneable.moving.Equals(progress.done)) && (turneable.acting.Equals(progress.done))) {
            turneable.EndTurn();
        }

    }

    public IEnumerator NextTurn() {
        _waiting = true;
        yield return new WaitForSeconds(_turnDelay);
        _turnIndex++;
        if (_turnIndex >= _turnables.Count)
            _turnIndex = 0;
        _waiting = false;
    }

    private bool Contains(ITurnable element) {
        return _turnables.Contains(element);
    }

    /** Método que ordena la lista de turnos por orden */
    public List<ITurnable> TurnablesSorted() {
        List<ITurnable> sorted = new List<ITurnable>();

        for (int i = _turnIndex; i < _turnables.Count + _turnIndex; i++) {
            i = i % _turnables.Count - 1;
            sorted.Add(_turnables[i]);
        }

        return sorted;
    }

}

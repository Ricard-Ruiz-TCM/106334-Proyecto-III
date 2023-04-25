using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    // Callback cuando modificando la lista
    public Action onEndTurn;
    public Action onModifyTurnList;

    [SerializeField]
    private int _index = 0;
    [SerializeField]
    private List<ITurnable> _turnables;

    [SerializeField, Header("Timing:")]
    private float _delay = 2f;

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

    public Actor CurrentTurnActor() {
        return _turnables[_index].actor;
    }

    // Unity Update
    private void Update() {
        ITurnable turneable = _turnables[_index];

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
            onEndTurn?.Invoke();
        }

    }

    public IEnumerator NextTurn() {
        _waiting = true;
        yield return new WaitForSeconds(_delay);
        _index++;
        if (_index >= _turnables.Count)
            _index = 0;
        _waiting = false;
    }

    private bool Contains(ITurnable element) {
        return _turnables.Contains(element);
    }

    /** Método que ordena la lista de turnos por orden */
    public List<ITurnable> TurnablesSorted() {
        List<ITurnable> sorted = new List<ITurnable>();

        for (int i = _index; sorted.Count != _turnables.Count; i++) {

            i = i % _turnables.Count;
            sorted.Add(_turnables[i]);
        }

        return sorted;
    }

}

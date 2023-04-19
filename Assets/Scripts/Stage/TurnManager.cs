using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

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
    }

    public void Remove(ITurnable element) {
        if (Contains(element))
            _turnables.Remove(element);
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

        if ((turneable.hasMoved) && (turneable.hasActed)) {
            turneable.EndTurn();
        }

    }

    public IEnumerator NextTurn() {
        _waiting = true;
        yield return new WaitForSeconds(_turnDelay);
        _turnIndex++;
        if (_turnIndex >= _turnables.Count) _turnIndex = 0;
        _waiting = false;
    }

    private bool Contains(ITurnable element) {
        return _turnables.Contains(element);
    }

}

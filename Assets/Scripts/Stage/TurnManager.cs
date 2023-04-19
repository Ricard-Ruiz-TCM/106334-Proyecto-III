using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    [SerializeField]
    private int _turnIndex = 0;
    [SerializeField]
    private List<ITurnable> _turnables;

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
        // Turno completado
        if (_turnables[_turnIndex].hasEnded) {
            NextTurn();
        }
    }

    public void NextTurn() {
        _turnIndex++;
        if (_turnIndex > _turnables.Count) _turnIndex = 0;
    }

    private bool Contains(ITurnable element) {
        return _turnables.Contains(element);
    }

}

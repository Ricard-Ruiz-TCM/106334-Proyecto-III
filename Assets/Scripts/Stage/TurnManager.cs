using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    private int _turn = 0;
    private List<ITurnElement> _turnList;

    private ITurnElement Element() { return _turnList[_turn]; }

    public void Add(ITurnElement element) {
        if (!Contains(element))
            _turnList.Add(element);
    }

    public void Remove(ITurnElement element) {
        if (Contains(element))
            _turnList.Remove(element);
    }

    // Unity Update
    private void Update() {
        // Turno completado
        if (Element().TurnCompleted) {
            NextTurn();
        }
    }

    public void NextTurn() {
        _turn++;
        if (_turn > _turnList.Count) _turn = 0;
    }

    private bool Contains(ITurnElement element) {
        return _turnList.Contains(element);
    }

}

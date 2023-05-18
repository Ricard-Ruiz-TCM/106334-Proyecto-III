using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    /** Singleton Instance */
    public static TurnManager instance = null;

    /** Callbacks */
    public Action onStartTurn;
    public Action onEndTurn;
    /** --------- */
    public Action<roundType> onNewRound;
    /** --------- */
    public Action<roundType> onEndRound;
    /** --------- */
    public Action onEndSystem;
    public Action onStartSystem;
    /** --------- */
    public Action onModifyAttenders;
    /** --------- */

    [SerializeField, Header("Actores que participan:")]
    protected int _current = 0;
    public Turnable current => _attenders[_current];
    [SerializeField]
    protected List<Turnable> _attenders = new List<Turnable>();
    public List<Turnable> Attenders => _attenders;

    [SerializeField, Header("Rondas:")]
    private roundType _roundType = roundType.thinking;
    [SerializeField]
    private int _rounds = 0;

    [SerializeField, Header("Tiempos:")]
    private float _endTurnDelaySecs = 1f;
    [SerializeField]
    private float _startTurnDelaySecs = 1f;

    // Unity Awake
    void Awake() {
        // Singleton
        if ((instance != null) && (instance != this)) {
            GameObject.Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    // Unity Update
    void Update() {
        switch (_roundType) {
            case roundType.positioning:
                // TODO // REFACTOR
                foreach (Turnable a in _attenders) {
                    if (a is ManualActor) {
                        Debug.Log("ACTOR MANUAL PERRO");
                    }
                }
                break;
            case roundType.combat:
                if (current is StaticActor) {
                    nextTurn();
                    return;
                }
                switch (current.state) {
                    case turnState.thinking:
                        if (!current.isTurnDone()) {
                            current.onTurn();
                        }
                    break;
                    case turnState.acting:
                        current.act();
                        break;
                    case turnState.moving:
                        current.move();
                        break;
                    case turnState.completed:
                        nextTurn();
                        break;
                }
                break;
        }
    }

    /** Subscribe to manager */
    public void subscribe(Turnable element) {
        if (!_attenders.Contains(element))
            _attenders.Add(element);

        onModifyAttenders?.Invoke();
    }

    /** UnSubscribe to manager */
    public void unsubscribe(Turnable element) {
        if (_attenders.Contains(element))
            _attenders.Remove(element);

        onModifyAttenders?.Invoke();
    }

    /** Check if contains */
    public bool contains(Turnable element) {
        return _attenders.Contains(element);
    }

    /** Método que ordena la lista tomando _current como inicio */
    public List<Turnable> sortedByIndex() {
        List<Turnable> sorted = new List<Turnable>();
        for (int i = _current; sorted.Count != _attenders.Count; i++) {
            i %= _attenders.Count;
            sorted.Add(_attenders[i]);
        }
        return sorted;
    }

    public roundType getRoundType() {
        return _roundType;
    }

    /** método para indicar si esta en una ronda concreta */
    public bool isRoundType(roundType type) {
        return _roundType.Equals(type);
    }

    /** Método para compeltar la ronda */
    public void completeRound() {
        completeRoundType(_roundType);
    }

    /** Método para indicar que ya hemos hecho el posicionamiento */
    public void completeRoundType(roundType type) {
        if (!isRoundType(type))
            return;

        switch (type) {
            case roundType.thinking:
                startManager();
                break;
            case roundType.positioning:
                _roundType = roundType.combat;
                onEndRound?.Invoke(_roundType);
                break;
            case roundType.combat:
                // TODO // Check IA de victorya/derrota
                break;
            case roundType.completed:
                break;
        }
    }

    /** Método para indicar que se acabo el combate dentro del stage */
    public void endManager() {
        _roundType = roundType.completed;
        onEndSystem?.Invoke();
    }

    /** Método para iniciar el sistema desde fuera */
    public void startManager() {
        _roundType = roundType.positioning;
        onStartSystem?.Invoke();
    }

    /** Control para el siguiente turno, añade delays y hace callbacks, plus control */
    private void nextTurn() {
        endTurn();
        StartCoroutine(changeTurn(_roundType));
    }

    /** Método para cambiar el turno del actor, añade delay al asunto */
    private IEnumerator changeTurn(roundType type) {
        _roundType = roundType.thinking;
        yield return new WaitForSeconds(_endTurnDelaySecs);
        _current++;
        if (_current >= _attenders.Count) {
            onNewRound?.Invoke(_roundType);
            _current = 0;
            _rounds++;
        }
        yield return new WaitForSeconds(_startTurnDelaySecs);
        _roundType = type;
        startTurn();
    }

    /** Método para acbar el turno */
    private void endTurn() {
        current.endTurn();
        onEndTurn?.Invoke();
    }

    /** Método para iniciar el turno */
    private void startTurn() {
        current.beginTurn();
        onStartTurn?.Invoke();
    }

}

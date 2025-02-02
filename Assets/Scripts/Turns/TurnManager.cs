using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    /** Singleton Instance */
    private static TurnManager _instance;
    public static TurnManager instance {
        get {
            return _instance;
        }
    }

    /** Callbacks */
    public static event Action onStartTurn;
    public static event Action onEndTurn;
    /** --------- */
    public static event Action<roundType> onNewRound;
    /** --------- */
    public static event Action<roundType> onEndRound;
    /** --------- */
    public static event Action<Turnable> onNewCurrentTurnable;
    /** --------- */
    public static event Action onEndSystem;
    public static event Action onStartSystem;
    /** --------- */
    public static event Action onModifyAttenders;
    /** --------- */

    //Check if it has been drawn already
    private bool haveDrawn = false;

    [SerializeField, Header("Actores que participan:")]
    protected int _current = 0;
    public Turnable current {
        get {
            if (_attenders.Count == 0)
                return null;

            return _attenders[Mathf.Clamp(_current, 0, _attenders.Count - 1)];
        }
    }
    [SerializeField]
    protected List<Turnable> _attenders = new List<Turnable>();
    public List<Turnable> attenders => _attenders;

    [SerializeField, Header("Rondas:")]
    private roundType _roundType = roundType.thinking;
    [SerializeField]
    private int _rounds = 0;
    public int getRounds() {
        return _rounds;
    }
    public bool isCombatStarted() {
        return getRounds() > 0;
    }

    [SerializeField, Header("Tiempos:")]
    private float _endTurnDelaySecs = 1f;
    [SerializeField]
    private float _startTurnDelaySecs = 1f;

    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            _instance.clear();
            Destroy(gameObject);
        }
    }

    // Unity Update
    void Update() {
        switch (_roundType) {
            case roundType.positioning:
                positioningRound();
                break;
            case roundType.combat:
                combatRounds();
                break;
        }
    }

    /** M�todo para limpiar el turn manager */
    public void clear() {
        _rounds = 0;
        _attenders.Clear();

        _roundType = roundType.thinking;

        onStartTurn = null;
        onEndTurn = null;
        onNewRound = null;
        onEndRound = null;
        onNewCurrentTurnable = null;
        onEndSystem = null;
        onStartSystem = null;
        onModifyAttenders = null;

    }

    /** M�todo para el roundType.positioning */
    private void positioningRound() {
        if (!haveDrawn) {
            FMODManager.instance.MakeDraws();
            haveDrawn = !haveDrawn;
        }
        ((ManualActor)uCore.GameManager.getPlayer()).positioning();
    }

    /** M�todo para el roundType.combat */
    private void combatRounds() {
        if (current is StaticActor) {
            nextTurn(true);
            return;
        }

        switch (current.state) {
            case turnState.thinking:
                if (!current.isTurnDone()) {
                    current.thinking();
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
    }

    /** Subscribe to manager */
    public void subscribe(Turnable element) {
        if (!_attenders.Contains(element))
            _attenders.Add(element);

        onModifyAttenders?.Invoke();
    }

    /** UnSubscribe to manager */
    public void unsubscribe(Turnable element) {
        if (_attenders.Count < 0)
            return;

        if (_attenders.Contains(element))
            _attenders.Remove(element);

        // Eliminar los elementos null de la lista
        _attenders.RemoveAll(item => item == null);

        onModifyAttenders?.Invoke();
    }

    /** Check if contains */
    public bool contains(Turnable element) {
        return _attenders.Contains(element);
    }

    /** M�todo que ordena la lista tomando _current como inicio */
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

    /** m�todo para indicar si esta en una ronda concreta */
    public bool isRoundType(roundType type) {
        return _roundType.Equals(type);
    }

    /** M�todo para compeltar la ronda */
    public void completeRound() {
        completeRoundType(_roundType);
    }

    /** M�todo para indicar que ya hemos hecho el posicionamiento */
    public void completeRoundType(roundType type) {
        if (!isRoundType(type))
            return;

        onEndRound?.Invoke(_roundType);
        switch (type) {
            case roundType.thinking:
                startManager();
                break;
            case roundType.positioning:
                _roundType = roundType.combat;
                startTurn();
                break;
            case roundType.completed:
                break;
        }
        onNewRound?.Invoke(_roundType);
    }

    /** M�todo para indicar que se acabo el combate dentro del stage */
    public void endManager() {
        _roundType = roundType.completed;
        onEndSystem?.Invoke();
    }

    /** M�todo para iniciar el sistema desde fuera */
    public void startManager() {
        _roundType = roundType.positioning;
        onStartSystem?.Invoke();
    }

    /** Control para el siguiente turno, a�ade delays y hace callbacks, plus control */
    private void nextTurn(bool skip = false) {
        StartCoroutine(changeTurn(_roundType, skip));
    }

    /** M�todo para cambiar el turno del actor, a�ade delay al asunto */
    private IEnumerator changeTurn(roundType type, bool skip) {
        endTurn();
        _roundType = roundType.thinking;
        if (!skip) {
            yield return new WaitForSeconds(_endTurnDelaySecs);
        }
        _current++;
        if (_current >= _attenders.Count) {
            onNewRound?.Invoke(_roundType);
            _current = 0;
            _rounds++;
        }
        onNewCurrentTurnable?.Invoke(current);
        if (!skip) {
            yield return new WaitForSeconds(_startTurnDelaySecs);
        }
        _roundType = type;
        startTurn();
    }

    /** M�todo para acbar el turno */
    private void endTurn() {
        current.endTurn();
        onEndTurn?.Invoke();
    }

    /** M�todo para iniciar el turno */
    private void startTurn() {
        current.beginTurn();
        onStartTurn?.Invoke();
    }

}

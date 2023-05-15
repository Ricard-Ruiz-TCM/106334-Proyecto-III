using System;
using System.Collections;
using UnityEngine;

public class TurnManager : ActorsListController {

    /** Singleton Instance */
    public static TurnManager instance = null;

    /** Callbacks */
    public Action onStartTurn;
    public Action onEndTurn;
    /** --------- */
    public Action onNewRound;
    /** --------- */
    public Action onStartRound;
    /** --------- */
    public Action onEndSystem;

    [SerializeField, Header("Rondas:")]
    private roundType _roundType = roundType.thinking;
    [SerializeField]
    private int _rounds = 0;

    [SerializeField, Header("Turno:")]
    private turnState _turnState = turnState.thinking;

    [SerializeField, Header("Tiempos:")]
    private float _endTurnDelaySecs = 2f;
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
                foreach (Actor a in _actors) {
                    if (a.CanBePlaced) {
                        a.GetComponent<Player>().Placing();
                    }
                }
                break;
            case roundType.combat:
                switch (_turnState) {
                    case turnState.ready:
                        // TODO // Decide what to do, acting or moving;
                        break;
                    case turnState.acting:
                        if (Current().CanAct()) {
                            Current().Act();
                        }
                        break;
                    case turnState.moving:
                        if (Current().CanMove()) {
                            Current().Move();
                        }
                        break;
                    case turnState.waiting:
                        if ((Current().moving.Equals(progress.done)) && (Current().acting.Equals(progress.done))) {
                            Current().EndTurn();
                            StartCoroutine(NextTurn());
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    /** Método para indicar si estoy en la ronda de posicionamiento */
    public bool isPositioningRound() {
        return _roundType.Equals(roundType.positioning);
    }

    /** Método para indicar si estoy en ronda de combate */
    public bool isCombatRound() {
        return _roundType.Equals(roundType.combat);
    }

    /** Método para indicar que ya hemos hecho el posicionamiento */
    public void positioningDone() {
        if (!_roundType.Equals(roundType.positioning))
            return;

        _roundType = roundType.combat;
        onStartRound?.Invoke();
        StartTurn();
    }

    /** Método para iniciar el turno */
    private void StartTurn() {
        _turnState = turnState.ready;
        Current().BeginTurn();
        onStartTurn?.Invoke();
    }

    /** Método para indicara que ya hemos leído el objetivo y pasamos al posicionamiento */
    public void ObjetiveRead() {
        if (!_roundType.Equals(roundType.thinking))
            return;

        _roundType = roundType.positioning;
    }

    /** Método para indicar que se acabo el combate dentro del stage */
    public void stageEnds() {
        _roundType = roundType.waiting;
        onEndSystem?.Invoke();
    }

    /** Método para iniciar el sistema desde fuera */
    public void startManager() {
        _roundType = roundType.thinking;
    }

    /** Control para el siguiente turno, añade delays y hace callbacks, plus control */
    private IEnumerator NextTurn() {
        // Fin de turno
        _turnState = turnState.thinking;
        onEndTurn?.Invoke();

        // Cambio de turno
        yield return new WaitForSeconds(_endTurnDelaySecs);
        _current++;
        if (_current >= _actors.Count) {
            onNewRound?.Invoke();
            _current = 0;
            _rounds++;
        }

        // Entrando al turno
        yield return new WaitForSeconds(_startTurnDelaySecs);
        StartTurn();
    }

}

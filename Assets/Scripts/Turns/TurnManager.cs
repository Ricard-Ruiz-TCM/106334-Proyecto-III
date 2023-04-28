using System;
using UnityEngine;
using System.Collections;

public class TurnManager : ActorsListController {

    /** Singleton Instance */
    public static TurnManager instance = null;

    /** Callbacks */
    public Action onStartTurn;
    public Action onEndTurn;
    public Action onNewRound;

    [SerializeField, Header("Rondas:")]
    private int _rounds = 0;

    [SerializeField, Header("Turn:")] // ready -> Turno comenzado, "entando" al turno | doing -> Haciendo el turno | done -> Turno acabado, "cambiando" de turno
    private progress _turnProgress = progress.done;

    [SerializeField, Header("Timing:")]
    private float _endTurnDelay = 2f;
    [SerializeField]
    private float _startTurnDelay = 1f;

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

        // DelayTime para el efectivo del turno
        if (_turnProgress.Equals(progress.done) || (_turnProgress.Equals(progress.ready)))
            return;

        // Hemos terminado nuestro turno, go next
        if (_actors[_current].hasTurnEnded) {
            StartCoroutine(NextTurn());
            return;
        }

        // Move Action
        if (_actors[_current].CanMove()) {
            _actors[_current].Move();
        }

        // Standart Action
        if (_actors[_current].CanAct()) {
            _actors[_current].Act();
        }

        // End Turn Check
        if ((_actors[_current].moving.Equals(progress.done)) && (_actors[_current].acting.Equals(progress.done))) {
            _actors[_current].EndTurn();
        }

    }

    /** Método para iniciar el sistema desde fuera */
    public void StartManager() {
        _turnProgress = progress.ready;
        _actors[_current].BeginTurn();
        onStartTurn?.Invoke();
    }

    /** Control para el siguiente turno, añade delays y hace callbacks, plus control */
    private IEnumerator NextTurn() {
        // Fin de turno
        _turnProgress = progress.done;
        onEndTurn?.Invoke();

        // Cambio de turno
        yield return new WaitForSeconds(_endTurnDelay);
        _turnProgress = progress.ready;
        _current++;
        if (_current >= _actors.Count) {
            onNewRound?.Invoke();
            _current = 0; _rounds++;
        }

        // Entrando al turno
        yield return new WaitForSeconds(_startTurnDelay);
        StartManager();
    }

}

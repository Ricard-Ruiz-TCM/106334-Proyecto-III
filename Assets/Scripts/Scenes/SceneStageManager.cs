using System;
using UnityEngine;
using System.Collections.Generic;

public class SceneStageManager : MonoBehaviour {

    [SerializeField, Header("Dialog:")]
    private GameObject _dialogUI;
    [SerializeField]
    private GameObject _upgradeUI;
    [SerializeField]
    private GameObject _perksUI;

    [SerializeField, Header("Combat:")]
    private GameObject _turnUI;
    [SerializeField]
    private GameObject _playerUI;

    [SerializeField, Header("Stage:")]
    private StageObjetiveUI _objetiveUI;
    [SerializeField]
    private StageResolutionUI _stageResolutionUI;

    [SerializeField, Header("Estado del stage:")]
    private stageState _state = stageState.innDialog;

    [SerializeField, Header("Información del Nivel:")]
    private Stage _stage;
    [SerializeField]
    private StageData _data;

    /** Escena actual */
    [SerializeField, Header("Escena:")]
    private gameScenes _thisScene;
    [SerializeField]
    private gameScenes _nextScene;

    [SerializeField, Header("Entidades con turno en la escena:")]
    private List<GameObject> _actors;

    // Unity OnEnable
    void OnEnable() {
        // DialogTriggers
        OnEndDialog.onTrigger += nextStageState;
        OnOpenPerkPanel.onTrigger += openPerkPanel;
        OnOpenUpgradePanel.onTrigger += openUpgradePanel;
        OnNextStageDialog.onTrigger += stageSuccess;

        Stage.onCompleteStage += completeStage;
    }

    // Unity OnDisable
    void OnDisable() {
        // DialogTriggers
        OnEndDialog.onTrigger -= nextStageState;
        OnOpenPerkPanel.onTrigger -= openPerkPanel;
        OnOpenUpgradePanel.onTrigger -= openUpgradePanel;
        OnNextStageDialog.onTrigger -= stageSuccess;

        Stage.onCompleteStage -= completeStage;
    }

    // Unity Start
    void Start() {

        // Check si tenemos que ir al combat directo
        if (_data.innitialDialog == null) {
            _state = stageState.shiftManaged;
        }

        // Set data to Stage
        _stage.SetData(_data);

        // Set del estado del estado, a funciones y cositas
        buildStage();

    }

    /** Método para actualizar el stage */
    private void buildStage() {

        // "Clear" del estado del stage
        _dialogUI.SetActive(false);
        _upgradeUI.SetActive(false);
        _perksUI.SetActive(false);
        _turnUI.SetActive(false);
        _playerUI.SetActive(false);
        _objetiveUI.gameObject.SetActive(false);
        _stageResolutionUI.gameObject.SetActive(false);

        // "Build" para los elementos
        switch (_state) {
            case stageState.innDialog:
                startDialog(_data.innitialDialog);
                break;
            case stageState.shiftManaged:
                startCombat();
                break;
            case stageState.endDialog:
                startDialog(_data.lastDialog);
                break;
            case stageState.completed:
                endStage();
                break;
            default:
                break;
        }
    }

    /** Método apra inicializar dialogo */
    private void startDialog(DialogNode node) {
        _dialogUI.SetActive(true);
        // Innit del dialogManager
        DialogManager.instance.startDialog(node);
    }

    /** Método para inicializar combate */
    private void startCombat() {
        _objetiveUI.gameObject.SetActive(true);
        _objetiveUI.SetObjetive(_data);
        _playerUI.SetActive(true);
        _turnUI.SetActive(true);

        // Activate the actors
        foreach (GameObject actors in _actors) {
            actors.SetActive(true);
        }

        // Innit del turnManager
        TurnManager.instance.startManager();
    }

    /** Método para mostrar el final del stage */
    public void endStage() {
        _stageResolutionUI.gameObject.SetActive(true);
    }

    /** Método para abrir el panel de upgrades del blacsmith */
    public void openUpgradePanel() {
        _dialogUI.SetActive(false);
        _upgradeUI.SetActive(true);
    }

    /** Método para abrir el panel de perks */
    public void openPerkPanel() {
        _dialogUI.SetActive(false);
        _perksUI.SetActive(true);
    }

    /** Método para indicar en que estado del stage estamos y pasar al siguiente con el EndNode */
    public void nextStageState() {
        _state = (stageState)((int)_state + 1);

        // Check si es final o terminamos??
        if (_state.Equals(stageState.endDialog)) {
            if (_data.lastDialog == null) {
                _state = stageState.completed;
            }
        }

        buildStage();
    }

    /** Método para determinar qeu se ha ganado el Stage */
    public void stageSuccess() {
        uCore.GameManager.SaveGameData();
        uCore.Director.LoadSceneAsync(_nextScene);
    }

    /** Método para determinar que se ha perdido el Stage */
    public void stageFailed() {
        uCore.GameManager.LoadGameData();
        uCore.Director.LoadSceneAsync(_thisScene);
    }

    /** Botones */
    public void BTN_StageSuccessUIClose() {
        stageSuccess();
    }
    /** ------- */

    /** Método para la resolución del stage */
    public void completeStage(stageResolution res) {
        _stageResolutionUI.SetResolution(res, stageSuccess, stageFailed);
        nextStageState();
    }

}

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
    private StageResolutionUI _stageResolution;

    [SerializeField, Header("Información del Nivel:")]
    private Stage _stage;
    [SerializeField]
    private StageData _data;

    [SerializeField, Header("Siguiente escena:")]
    private gameScenes _nextScene;

    [SerializeField, Header("Entidades con turno en la escena:")]
    private List<GameObject> _actors;

    // Unity OnEnable
    void OnEnable() {
        // DialogTriggers
        OnEndDialog.onTrigger += StageSuccess;
        OnOpenPerkPanel.onTrigger += OpenPerkPanel;
        OnOpenUpgradePanel.onTrigger += OpenUpgradePanel;
        // Player Die
        //Player.onPlayerDie += () => { CompleteStage(stageResolution.defeat); };
        // Player Reach Destinty
        //Player.onPlayerReachObjetive += () => { CompleteStage(stageResolution.victory); };
        // Completation
        Stage.onCompleteStage += CompleteStage;
    }

    // Unity OnDisable
    void OnDisable() {
        // DialogTriggers
        OnEndDialog.onTrigger -= StageSuccess;
        OnOpenPerkPanel.onTrigger -= OpenPerkPanel;
        OnOpenUpgradePanel.onTrigger -= OpenUpgradePanel;
        // Player Die
        //Player.onPlayerDie -= () => { CompleteStage(stageResolution.defeat); };
        // Player Reach Destinty
        //Player.onPlayerReachObjetive -= () => { CompleteStage(stageResolution.victory); };
        // Completation
        Stage.onCompleteStage -= CompleteStage;
    }

    // Unity Start
    void Start() {
        // Set data to Stage
        _stage.SetData(_data);

        // Build the Stage
        switch (_data.type) {
            case stageType.combat:
                EnableCombat(_data);
                break;
            case stageType.comrade:
            case stageType.blacksmith:
            case stageType.campfire:
                EnableDialog(_data);
                break;
        }

        // Activate the actors
        foreach (GameObject actors in _actors) {
            actors.SetActive(true);
        }

    }

    /** Métodos para habilitar los managers necesarios del stage */
    private void EnableDialog(StageData data) {
        _dialogUI.SetActive(true);
        // Innit del dialogManager
        DialogManager.instance.startDialog(data.innitialDialog);
    }

    private void EnableCombat(StageData data) {
        _objetiveUI.gameObject.SetActive(true);
        _objetiveUI.SetObjetive(data);
        _playerUI.SetActive(true);
        _turnUI.SetActive(true);
        // Innit del turnManager
        TurnManager.instance.startManager();
    }
    /** -------------------------------------------------------- */

    /** Método para abrir el panel de upgrades del blacsmith */
    public void OpenUpgradePanel() {
        _dialogUI.SetActive(false);
        _upgradeUI.SetActive(true);
    }

    /** Método para abrir el panel de perks */
    public void OpenPerkPanel() {
        _dialogUI.SetActive(false);
        _perksUI.SetActive(true);
    }

    /** Método para determinar qeu se ha ganado el Stage */
    public void StageSuccess() {
        uCore.GameManager.SaveGameData();
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

    /** Método para determinar que se ha perdido el Stage */
    public void StageFailed() {
        uCore.GameManager.StageSelected(uCore.GameManager.LastStage);
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

    /** Botones */
    public void BTN_StageUIClose() {
        StageSuccess();
    }
    /** ------- */

    /** Método para la resolución del stage */
    public void CompleteStage(stageResolution res) {
        _playerUI.SetActive(false);
        _stageResolution.SetResolution(res, StageSuccess, StageFailed);
        _stageResolution.gameObject.SetActive(true);
        // Cargando escena
        uCore.Director.LoadSceneAsync(_nextScene);
    }

}

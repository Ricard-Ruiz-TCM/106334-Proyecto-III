using System;
using UnityEngine;

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

    [Header("TESTING:")]
    public StageData stageData;

    // Unity Start
    void Start() {
        StageData data = stageData;

        // Build the Stage
        switch (data.type) {
            case stageType.combat:
                EnableCombat(data);
                break;
            case stageType.comrade:
            case stageType.blacksmith:
            case stageType.campfire:
                EnableDialog(data);
                break;
        }

        // Check si hay modificación de la lista de actores cuando empiezan las rondas
        /*TurnManager.instance.onEndRound += () => {
            TurnManager.instance.onModifyAttenders += Stage.CheckStage;
        };*/
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
        // Innit del stageLoader
        // TODO // StageLoader.instance.buildStage(data);
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
    }


}

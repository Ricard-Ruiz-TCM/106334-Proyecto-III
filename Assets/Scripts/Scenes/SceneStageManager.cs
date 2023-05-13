using System;
using UnityEngine;

public class SceneStageManager : MonoBehaviour {

    /** Observer para completation del stage */
    public static event Action OnCompleteStage;

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

    [SerializeField, Header("Stage: Data")]
    private StageData _data;

    // Unity OnEnable
    void OnEnable() {
        // DialogTriggers
        OnEndDialog.onTrigger += StageSuccess;
        OnOpenPerkPanel.onTrigger += OpenPerkPanel;
        OnOpenUpgradePanel.onTrigger += OpenUpgradePanel;

        Player.onPlayerDie += () => { CompleteStage(stageResolution.defeat); };
    }

    // Unity OnDisable
    void OnDisable() {
        // DialogTriggers
        OnEndDialog.onTrigger -= StageSuccess;
        OnOpenPerkPanel.onTrigger -= OpenPerkPanel;
        OnOpenUpgradePanel.onTrigger -= OpenUpgradePanel;

        Player.onPlayerDie -= () => { CompleteStage(stageResolution.defeat); };
    }

    // TESTING
    [Header("TESTING:")]
    public Stage stageTest;

    // Unity Start
    void Start() {
        _data = uCore.GameManager.NextStage;

        // TESTING
        _data = stageTest.Data;

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

        TurnManager.instance.onStartRounds += () => {
            TurnManager.instance.onModifyList += CheckCompletation;
        };
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
        StageLoader.instance.buildStage(data);
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

        OnCompleteStage?.Invoke();
    }

    public void CheckCompletation() {
        bool completed = true;
        switch (_data.objetive) {
            case stageObjetive.killAll:
                foreach (Actor actor in TurnManager.instance.Actors) {
                    if (actor is Enemy) {
                        completed = false;
                    }
                }
                break;
            case stageObjetive.protectNPC:
                Debug.Log("TAMOS JODIDOS PERO CON EL NPC");
                break;
            case stageObjetive.clearPath:
                Debug.Log("TAMOS JODIDOS");
                break;
        }

        if (completed) {
            CompleteStage(stageResolution.victory);
        }
    }

}

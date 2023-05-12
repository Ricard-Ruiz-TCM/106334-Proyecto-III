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

    // Unity OnEnable
    void OnEnable() {
        // DialogTriggers
        OnEndDialog.onTrigger += StageSuccess;
        OnOpenPerkPanel.onTrigger += OpenPerkPanel;
        OnOpenUpgradePanel.onTrigger += OpenUpgradePanel;
    }

    // Unity OnDisable
    void OnDisable() {
        // DialogTriggers
        OnEndDialog.onTrigger -= StageSuccess;
        OnOpenPerkPanel.onTrigger -= OpenPerkPanel;
        OnOpenUpgradePanel.onTrigger -= OpenUpgradePanel;
    }

    // TESTING
    [Header("TESTING:")]
    public Stage stageTest;

    // Unity Start
    void Start() {
        StageData stage = uCore.GameManager.NextStage;

        // TESTING
        stage = stageTest.Data;

        switch (stage.type) {
            case stageType.combat:
                EnableCombat(stage);
                break;
            case stageType.comrade:
            case stageType.blacksmith:
            case stageType.campfire:
                EnableDialog(stage);
                break;
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

}

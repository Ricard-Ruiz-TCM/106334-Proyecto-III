using UnityEngine;

public class SceneStageManager : MonoBehaviour {

    [SerializeField, Header("Dialog:")]
    private GameObject _dialogUI;
    [SerializeField]
    private GameObject _upgradeUI;
    [SerializeField, Header("Combat:")]
    private GameObject _combatUI;

    // Unity OnEnable
    void OnEnable() {
        // DialogTriggers
        OnEndDialog.onTrigger += StageSuccess;
        OnOpenUpgradePanel.onTrigger += OpenUpgradePanel;
    }

    // Unity OnDisable
    void OnDisable() {
        // DialogTriggers
        OnEndDialog.onTrigger -= StageSuccess;
        OnOpenUpgradePanel.onTrigger -= OpenUpgradePanel;
    }

    // Unity Awake
    void Start() {
        StageData stage = uCore.GameManager.NextStage;

        switch (stage.type) {
            case stageType.combat:
                //EnableCombat();
                StageSuccess();
                break;
            case stageType.comrade:
            case stageType.blacksmith:
            case stageType.campfire:
                EnableDialog(stage.innitialDialog);
                break;
        }

    }

    /** Métodos para habilitar los managers necesarios del stage */
    private void EnableDialog(DialogNode node) {
        _dialogUI.SetActive(true);
        // Innit del dialogManager
        DialogManager.instance.StartDialog(node);
    }
    private void EnableCombat() {
        _combatUI.SetActive(true);
        // Innit del stageLoader
        GameObject.FindAnyObjectByType<StageLoader>().BuildStage();
        GameObject.FindAnyObjectByType<StageLoader>().BuildPlayer();
        // Innit del turnManager
        TurnManager.instance.startManager();
    }
    /** -------------------------------------------------------- */

    /** Método para abrir el panel de upgrades del blacsmith */
    public void OpenUpgradePanel() {
        _dialogUI.SetActive(false);
        _upgradeUI.SetActive(true);
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
    public void BTN_UpgradePanelCloase() {
        StageSuccess();
    }
    /** ------- */

}

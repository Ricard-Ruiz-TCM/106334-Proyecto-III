using UnityEngine;

public class SceneStageManager : MonoBehaviour {

    public void BTN_Menu() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }

    public void BTN_StageSelector() {
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

    [SerializeField, Header("Dialogue:")]
    private GameObject _dialogue;

    [SerializeField, Header("Comrade:")]
    private GameObject _comrade;
    [SerializeField]
    private GameObject _complete;

    [SerializeField, Header("Blacksmith:")]
    private GameObject _blacksmith;
    [SerializeField]
    private GameObject _upgrades;
    [SerializeField]
    private GameObject _shop;

    // Unity OnEnable
    private void OnEnable() {

    }

    // Unity OnDisable
    private void OnDisable() {

    }

    private void PerkPanel() {
        HideDialogue();
        _comrade.SetActive(true);
    }

    private void HideDialogue() {
        _dialogue.SetActive(false);
    }

    private void EndEvent() {
        uCore.Director.LoadSceneAsync(gameScenes.Stage);
    }

    public void BTN_CompletePerks() {
        _complete.SetActive(true);
    }

    public void BTN_Cancel() {
        _complete.SetActive(false);
    }

    public void BTN_OpenBlacksmithOptions() {
        _blacksmith.SetActive(false);
        _shop.SetActive(false);
        _upgrades.SetActive(false);
        _dialogue.SetActive(true);
        GameObject.FindAnyObjectByType<DialogManager>().StartDialog(uCore.GameManager.BlacksmithNode.nextNode);
    }

    public void BTN_Sure() {
        EndEvent();
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

}

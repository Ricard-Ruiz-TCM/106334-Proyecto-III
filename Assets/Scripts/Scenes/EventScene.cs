using UnityEngine;

public class EventScene : MonoBehaviour {

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
        OpenPerkPanel.openPerkPanel += PerkPanel;
        OpenShopPanel.openShopPanel += ShopPanel;
        OpenUpgradePanel.openUpgradePanel += UpgradePanel;

        EndRoadEvent.endRoadEvent += EndEvent;
    }

    // Unity OnDisable
    private void OnDisable() {
        OpenPerkPanel.openPerkPanel -= PerkPanel;
        OpenShopPanel.openShopPanel -= ShopPanel;
        OpenUpgradePanel.openUpgradePanel -= UpgradePanel;

        EndRoadEvent.endRoadEvent -= EndEvent;
    }

    // Unity Start
    void Start() {
        switch (uCore.GameManager.RoadEvent) {
            case roadEvent.blacksmith:
                GameObject.FindAnyObjectByType<DialogManager>().StartDialog(uCore.GameManager.BlacksmithNode);
                break;
            case roadEvent.comrade:
                GameObject.FindAnyObjectByType<DialogManager>().StartDialog(uCore.GameManager.ComradeNode);
                break;
            default: break;
        }
    }

    private void PerkPanel() {
        HideDialogue();
        _comrade.SetActive(true);
    }

    private void ShopPanel() {
        HideDialogue();
        _blacksmith.SetActive(true);
        _shop.SetActive(true);
    }

    private void UpgradePanel() {
        HideDialogue();
        _blacksmith.SetActive(true);
        _upgrades.SetActive(true);
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

}

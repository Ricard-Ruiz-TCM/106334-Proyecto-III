using UnityEngine;

public class EventScene : MonoBehaviour
{

    [SerializeField, Header("Dialogue:")]
    private GameObject _dialogue;

    [SerializeField, Header("Comrade:")]
    private GameObject _comrade;
    [SerializeField]
    private GameObject _complete;

    [SerializeField, Header("Blacksmith:")]
    private GameObject _blacksmith;

    // Unity OnEnable
    private void OnEnable()
    {
        OpenPerkPanel.openPerkPanel += PerkPanel;
        OpenShopPanel.openShopPanel += ShopPanel;
        OpenUpgradePanel.openUpgradePanel += UpgradePanel;

        EndRoadEvent.endRoadEvent += EndEvent;
    }

    // Unity OnDisable
    private void OnDisable()
    {
        OpenPerkPanel.openPerkPanel -= PerkPanel;
        OpenShopPanel.openShopPanel -= ShopPanel;
        OpenUpgradePanel.openUpgradePanel -= UpgradePanel;

        EndRoadEvent.endRoadEvent -= EndEvent;
    }

    // Unity Start
    void Start()
    {
        switch (uCore.GameManager.RoadEvent)
        {
            case roadEvent.blacksmith:
                GameObject.FindAnyObjectByType<DialogueManager>().StartDialogue(uCore.GameManager.BlacksmithNode);
                break;
            case roadEvent.comrade:
                GameObject.FindAnyObjectByType<DialogueManager>().StartDialogue(uCore.GameManager.ComradeNode);
                break;
            default: break;
        }
    }

    private void PerkPanel()
    {
        HideDialogue();
        _comrade.SetActive(true);
    }

    private void ShopPanel()
    {
        HideDialogue();
        _blacksmith.SetActive(true);
    }

    private void UpgradePanel()
    {
        HideDialogue();
        _blacksmith.SetActive(true);
    }

    private void HideDialogue()
    {
        _dialogue.SetActive(false);
    }

    private void EndEvent()
    {
        uCore.Director.LoadSceneAsync(gameScenes.Stage);
    }

    public void BTN_CompletePerks()
    {
        _complete.SetActive(true);
    }

    public void BTN_Cancel()
    {
        _complete.SetActive(false);
    }

    public void BTN_Sure()
    {
        EndEvent();
    }

}

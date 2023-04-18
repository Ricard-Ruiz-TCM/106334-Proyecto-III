using UnityEngine;

public class InformantScene : MonoBehaviour {

    [SerializeField, Header("Dialogues:")]
    private GameObject _dialogue;
    private DialogueManager _dialogueManager;

    [SerializeField, Header("Informant Panel:")]
    private GameObject _informant;
    [SerializeField]
    private GameObject _complete;

    // Unity OnEnable
    private void OnEnable() {
        DialogueManager.onEndDialogue += DisplayInformant;
    }

    // Unity OnDisable
    private void OnDisable() {
        DialogueManager.onEndDialogue -= DisplayInformant;
    }

    // Unity Awake
    void Awake() {
        _dialogueManager = _dialogue.GetComponent<DialogueManager>();
    }

    public DialogueNode test;
    // Unity Start
    void Start() {
        _dialogue.SetActive(true);
        _informant.SetActive(false);
        _complete.SetActive(false);
        _dialogueManager.StartDialogue(test);
    }

    // Desactiva el dialogo y activa el panel del informante
    private void DisplayInformant() {
        _informant.SetActive(true);
        _dialogue.SetActive(false);
    }

    public void BTN_CompletePerks() {
        _complete.SetActive(true);
    }

    public void BTN_Cancel() {
        _complete.SetActive(false);
    }

    public void BTN_Sure() {
        uCore.Director.LoadSceneAsync(gameScenes.Stage);
    }

}

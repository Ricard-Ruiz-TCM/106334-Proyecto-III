using UnityEngine;

public class EventScene : MonoBehaviour {

    [SerializeField, Header("Dialogues:")]
    private GameObject _dialogue;
    private DialogueManager _dialogueManager;

    [SerializeField, Header("Comrade Panel:")]
    private GameObject _comrade;
    [SerializeField]
    private GameObject _complete;

    // Unity OnEnable
    private void OnEnable() {
        DialogueManager.onEndDialogue += DisplayComrade;
    }

    // Unity OnDisable
    private void OnDisable() {
        DialogueManager.onEndDialogue -= DisplayComrade;
    }

    // Unity Awake
    void Awake() {
        _dialogueManager = _dialogue.GetComponent<DialogueManager>();
    }

    public DialogueNode test;
    // Unity Start
    void Start() {
        _dialogue.SetActive(true);
        _comrade.SetActive(false);
        _complete.SetActive(false);
        _dialogueManager.StartDialogue(test);
    }

    // Desactiva el dialogo y activa el panel del comrade
    private void DisplayComrade() {
        _comrade.SetActive(true);
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {

    [SerializeField, Header("Speakers:")]
    private Image _speaker;

    [SerializeField, Header("Texts:")]
    private GameObject _panelText;
    [SerializeField]
    private TextMeshProUGUI _txtSpeakerName;
    [SerializeField]
    private TextMeshProUGUI _txtSpeakerText;
    [SerializeField]
    private string txtNameKey;
    [SerializeField]
    private string txtDescriptKey;
    [SerializeField]
    private bool myTurn;
    [SerializeField]
    private TutorialUI nextPopUp;
    [SerializeField]
    private float timePerCharacter = 5f;
    [SerializeField]
    private bool lastTutorialPopUp = false;

    private MovementStopper movementStopper;
    private bool roundsHaveStarted = false;
    private bool textSetted = false;
    private int writterId;

    private void Start()
    {
        movementStopper = gameObject.GetComponent<MovementStopper>();
        writterId = 0;
    }

    private void Update() {
        if (roundsHaveStarted && myTurn && !textSetted) {
            textSetted = true;
            setText();
        }

    }

    public bool getMyTurnValue() {
        return myTurn;
    }

    public void setMyTurn() {
        myTurn = true;
    }

    private void setText() {
        writterId = TutorialTextWritter.AddWritter_Static(_txtSpeakerName, _txtSpeakerText, _txtSpeakerName.text = uCore.Localization.GetText(txtNameKey), _txtSpeakerText.text = uCore.Localization.GetText(txtDescriptKey), timePerCharacter);
    }

    private void destroyWritterInstance()
    {
        TutorialTextWritter.setSetDestroyInstance(writterId);
    }

    public void setRoundsHaveStarted() {
        roundsHaveStarted = true;
    }

    // Clickar en el texto del dialogo
    public void onClickSetNext() {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.PressButtonUI);
        if (nextPopUp != null) {
            nextPopUp.gameObject.SetActive(true);
            nextPopUp.setMyTurn();
            nextPopUp.setText();
        }
        if (lastTutorialPopUp && movementStopper != null)
        {
            movementStopper.OnPointerExit(null);
        }
        destroyWritterInstance();
        gameObject.SetActive(false);
    }

}


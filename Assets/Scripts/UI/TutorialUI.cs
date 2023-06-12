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

    private bool roundsHaveStarted = false;
    private bool textSetted = false;


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
        TutorialTextWritter.AddWritter_Static(_txtSpeakerName, _txtSpeakerText, _txtSpeakerName.text = uCore.Localization.GetText(txtNameKey), _txtSpeakerText.text = uCore.Localization.GetText(txtDescriptKey), timePerCharacter);
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
        gameObject.SetActive(false);
    }

}


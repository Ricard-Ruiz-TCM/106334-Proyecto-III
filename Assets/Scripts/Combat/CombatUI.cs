using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour {

    [SerializeField]
    private UIText _txtName;

    [SerializeField]
    private UIText _txtHealth;
    [SerializeField]
    private UIText _txtDefense;
    [SerializeField]
    private UIText _txtMovement;

    [SerializeField]
    private Button _btnEndTurn;

    [SerializeField]
    private Transform _panelSkills;

    [SerializeField]
    private GameObject _skillButtonUI;

    private TurnManager _turnManager;

    private Actor _currentActor;

    // Unity Awake
    void Awake() {
        _turnManager = GameObject.FindObjectOfType<TurnManager>();
        // Set callback para actualizar el panel
        _turnManager.onEndTurn += UpdatePanel;
        // SetButton
        _btnEndTurn.onClick.AddListener(() => { _turnManager.EndTurn(); });
    }

    // Unity Update
    void Update() {
        if (_currentActor is Player)
            _txtMovement.UpdateText(_currentActor.Movement());
    }

    private void UpdatePanel() {

        _currentActor = _turnManager.CurrentTurnActor();

        _txtName.UpdateText(_currentActor.gameObject.name);

        if (_currentActor is Player) {
            _txtHealth.UpdateText(_currentActor.Health());
            _txtDefense.UpdateText(_currentActor.Defense());
            ClearSkills();
            foreach (SkillItem skI in _currentActor.Skills()) {
                GameObject.Instantiate(_skillButtonUI, _panelSkills).GetComponent<SkillButtonUI>().Set(skI.skill);
            }
        } else {
            foreach (Transform child in _panelSkills) {
                child.GetComponent<Button>().interactable = false;
            }
        }

        _btnEndTurn.interactable = (_currentActor is Player);

    }

    public void ClearSkills() {
        foreach (Transform child in _panelSkills) {
            GameObject.Destroy(child.gameObject);
        }
    }



}

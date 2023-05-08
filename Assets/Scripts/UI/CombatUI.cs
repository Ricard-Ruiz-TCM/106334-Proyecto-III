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

    private Actor _currentActor;

    // Unity OnEnable
    void OnEnable() {
        TurnManager.instance.onEndTurn += UpdatePanel;
    }

    // Unity OnDisable
    void OnDisable() {
        TurnManager.instance.onEndTurn -= UpdatePanel;
    }

    // Unity Update
    void Update() {
        // SetButton
        _btnEndTurn.onClick.AddListener(() => { TurnManager.instance.Current().EndTurn(); });

        if (_currentActor is Player) {
            _txtMovement.UpdateText(_currentActor.Movement());
            _txtHealth.UpdateText(_currentActor.Health());
            _txtDefense.UpdateText(_currentActor.Defense());
        }
    }

    private void UpdatePanel() {

        _currentActor = TurnManager.instance.Current();

        _txtName.UpdateText(_currentActor.gameObject.name);

        if (_currentActor is Player) {
            ClearSkills();
            foreach (SkillItem skI in _currentActor.Skills()) {
                GameObject btn = GameObject.Instantiate(_skillButtonUI, _panelSkills);
                btn.GetComponent<SkillButtonUI>().Set(_currentActor, skI);
                btn.GetComponent<Button>().interactable = (skI.cooldown <= 0);
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

using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private Button _btnEndTurn;

    [SerializeField]
    private Transform _panelSkills;

    [SerializeField]
    private GameObject _skillButtonUI;

    private Actor _currentActor;

    // Unity OnEnable
    void OnEnable() {
        TurnManager.instance.onStartTurn += UpdatePanel;
    }

    // Unity OnDisable
    void OnDisable() {
        TurnManager.instance.onStartTurn -= UpdatePanel;
    }

    // Unity Update
    void Update() {
        // SetButton
        _btnEndTurn.onClick.AddListener(() => {
            Actor player = TurnManager.instance.Current();
            if (player is Player) {
                if (!player.hasTurnEnded) { 
                    player.EndTurn();
                }
            }
        });
    }

    private void UpdatePanel() {

        _currentActor = TurnManager.instance.Current();

        if (_currentActor is Player) {
            ClearSkills();
            foreach (SkillItem skI in _currentActor.GetComponent<ActorSkills>().Skills()) {
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

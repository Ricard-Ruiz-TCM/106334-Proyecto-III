using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private Button _btnEndTurn;

    [SerializeField]
    private Transform _panelSkills;

    [SerializeField]
    private GameObject _skillButtonUI;

    [SerializeField]
    private Image _imgHealth;

    private Actor _currentActor;

    // Unity OnEnable
    void OnEnable() {
        TurnManager.instance.onStartTurn += UpdateSkills;
        TurnManager.instance.onStartTurn += UpdateHP;
    }

    // Unity OnDisable
    void OnDisable() {
        TurnManager.instance.onStartTurn -= UpdateSkills;
        TurnManager.instance.onStartTurn -= UpdateHP;
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

    private void UpdateHP() {
        _currentActor = TurnManager.instance.Current();

        if (_currentActor is Player) {
            _imgHealth.fillAmount = _currentActor.GetHealth() / _currentActor.MaxHealth();
        }
    }

    private void UpdateSkills() {

        _currentActor = TurnManager.instance.Current();

        int i = 1;
        if (_currentActor is Player) {
            ClearSkills();
            foreach (SkillItem skI in _currentActor.GetComponent<ActorSkills>().Skills()) {
                GameObject btn = GameObject.Instantiate(_skillButtonUI, _panelSkills);
                btn.GetComponent<SkillButtonUI>().Set(_currentActor, skI, (KeyCode)(((int)KeyCode.Alpha0) + i), i);
                btn.GetComponent<Button>().interactable = (skI.cooldown <= 0);
                i++;
            }
        } else {
            foreach (Transform child in _panelSkills) {
                child.GetComponent<Button>().interactable = false;
            }
        }

        if (i < 9) {
            for (int a = i; a < 9; a++) {
                GameObject btn = GameObject.Instantiate(_skillButtonUI, _panelSkills);
                btn.GetComponent<Button>().interactable = false;
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

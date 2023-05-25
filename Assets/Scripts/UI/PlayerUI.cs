using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private Button _btnEndTurn;

    [SerializeField]
    private Transform _panelSkills;

    [SerializeField]
    private GameObject _skillButtonUI;

    [SerializeField]
    private Image _imgHealth;

    [SerializeField]
    private UIText _steps, _defense;

    [SerializeField]
    private PlayerTurnInfoUI _playerTurnInfo;

    private Actor _currentTurnable;

    private List<SkillButtonUI> _skillButtons = new List<SkillButtonUI>();

    private void OnEnable() {
        SkillManager.onSkillUsed += UpdateSingleSkill;
    }

    private void Start() {
        TurnManager.instance.onStartTurn += UpdateUI;
        TurnManager.instance.onEndRound += (roundType t) => {
            if (t.Equals(roundType.positioning)) {
                UpdateUI();
            }
        };
    }

    public void BTN_EndTurn() {
        if (((Actor)TurnManager.instance.current).CompareTag("Player")) {
            ((Actor)TurnManager.instance.current).endTurn();
        }
    }

    public void UpdateUI() {
        
        if (!(TurnManager.instance.current is StaticActor)) {
            _currentTurnable = (Actor)TurnManager.instance.current;
        }

        UpdateHP();
        UpdateSkills();
        UpdateStats();
        UpdateSteps();
        UpdateDefense();
    }

    private void UpdateDefense() {
        if (_currentTurnable.CompareTag("Player")) {
            _defense.UpdateText(_currentTurnable.totalDefense());
        }
    }

    private void UpdateStats() {
        if (_currentTurnable.CompareTag("Player")) {
            _playerTurnInfo.UpdatePanel(_currentTurnable);
        }
    }

    private void UpdateSteps() {
        if (_currentTurnable.CompareTag("Player")) {
            _defense.UpdateText(_currentTurnable.stepsRemain());
        }
    }

    private void UpdateHP() {
        if (_currentTurnable.CompareTag("Player")) {
            _imgHealth.fillAmount = (_currentTurnable.healthPercent() / 100);
        }
    }

    private void UpdateSingleSkill(skillID iD) {
        foreach (SkillButtonUI sbui in _skillButtons) {
            if (sbui.SkItem.skill.ID.Equals(iD)) {
                sbui.UpdateCooldown();
            }
        }
    }

    private void UpdateSkills() {
        _skillButtons.Clear();
        int i = 1;
        if (_currentTurnable is Actor) {
            ClearSkills();
            foreach (SkillItem skI in _currentTurnable.skills.skills) {
                GameObject btn = GameObject.Instantiate(_skillButtonUI, _panelSkills);
                _skillButtons.Add(btn.GetComponent<SkillButtonUI>());
                btn.GetComponent<SkillButtonUI>().Set(_currentTurnable, skI, (KeyCode)(((int)KeyCode.Alpha0) + i), i);
                btn.GetComponent<Button>().interactable = (skI.cooldown < 0);
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

        _btnEndTurn.interactable = (_currentTurnable.CompareTag("Player"));

    }

    public void ClearSkills() {
        foreach (Transform child in _panelSkills) {
            GameObject.Destroy(child.gameObject);
        }
    }


}

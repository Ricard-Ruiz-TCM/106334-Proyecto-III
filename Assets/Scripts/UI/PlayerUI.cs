using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField, Header("Stats:")]
    private Image _imgHealth;
    [SerializeField]
    private UIText _steps, _defense;

    [SerializeField, Header("Buffs:")]
    private GameObject _shortBuffPfb;
    [SerializeField]
    private Transform _panelBuffs;

    [SerializeField, Header("Skills:")]
    private GameObject _skillButtonPfb;
    [SerializeField]
    private Transform _panelSkills;

    [SerializeField, Header("EndTurn Button:")]
    private Button _btnEndTurn;

    /** Reff del player */
    private Actor _player = null;

    /** Listas de instanciados */
    private Dictionary<skillID, SkillButtonUI> _skillButtons = new Dictionary<skillID, SkillButtonUI>();

    // Unity OnEnable
    private void OnEnable() {
        BuffManager.onApplyBuff += displayBuffs;
        BuffManager.onRemoveBuff += displayBuffs;

        Actor.onStepReached += (Node n) => { updateSteps(); };
        BasicActor.onChangeHealth += updateHealth;

        SkillManager.onSkillUsed += updateSingleSkill;
    }

    // Unity OnDisable
    private void OnDisable() {
        BuffManager.onApplyBuff -= displayBuffs;
        BuffManager.onRemoveBuff -= displayBuffs;

        Actor.onStepReached -= (Node n) => { updateSteps(); };
        BasicActor.onChangeHealth -= updateHealth;

        SkillManager.onSkillUsed -= updateSingleSkill;
    }

    // Unity Start
    private void Start() {
        TurnManager.instance.onModifyAttenders += getPlayer;    
    }

    /** M�todo que asigna el player al sistema, para los posibles jugadores */
    private void getPlayer() {
        Actor actor = (Actor)TurnManager.instance.current;
        // Set del player
        if (actor.CompareTag("Player")) {
            _player = actor;

            // Update the full HUD
            updateStats();
            displayBuffs();
            displaySkills();
        }
    }
   
    /** Update de todas las stats */
    private void updateStats() {
        updateSteps();
        updateHealth();
        updateDefense();
    }

    /** M�todo que actualiza la defensa total */
    private void updateDefense() {
        _defense.UpdateText(_player.totalDefense());
    }

    /** M�todo que actualiz ael hud de vida */
    private void updateHealth() {
        _imgHealth.fillAmount = (_player.healthPercent() / 100);
    }

    /** M�todo que acutaliza los steps */
    private void updateSteps() {
        _steps.UpdateText(_player.stepsRemain());
    }

    /** Update de uan sola skill */
    private void updateSingleSkill(skillID id) {
        if (!_skillButtons.ContainsKey(id))
            return;

        _skillButtons[id].UpdateCooldown();
    }

    /** M�todo para instanciar los botons de skills */
    private void displaySkills() {
        _skillButtons.Clear();
        int i = 1;
        if (_player is Actor) {
            clearPanel(_panelSkills);
            foreach (SkillItem skI in _player.skills.skills) {
                GameObject btn = GameObject.Instantiate(_skillButtonPfb, _panelSkills);
                _skillButtons.Add(skI.skill.ID, btn.GetComponent<SkillButtonUI>());
                btn.GetComponent<SkillButtonUI>().Set(_player, skI, (KeyCode)(((int)KeyCode.Alpha0) + i), i);
                btn.GetComponent<Button>().interactable = (skI.cooldown < 0);
                i++;
            }
        } else {
            foreach (Transform child in _panelSkills) {
                child.GetComponent<Button>().interactable = false;
            }
        }

        // Instant de las skills vac�as
        if (i < 9) {
            for (int a = i; a < 9; a++) {
                GameObject btn = GameObject.Instantiate(_skillButtonPfb, _panelSkills);
                btn.GetComponent<Button>().interactable = false;
            }
        }

        _btnEndTurn.interactable = (_player.CompareTag("Player"));
    }

    /** M�todo para mostar los buffos del player */
    public void displayBuffs() {
        clearPanel(_panelBuffs);
        for (int i = 0; i < _player.buffs.activeBuffs.Count && i < 6; i++) {
            GameObject.Instantiate(_shortBuffPfb, _panelBuffs).GetComponent<ShortStatusPanelUI>().UpdateStatus(_player.buffs.activeBuffs[i]);
        }
    }

    /** M�todo para destruir los hijos del elemento */
    public void clearPanel(Transform panel) {
        foreach (Transform child in panel) {
            GameObject.Destroy(child.gameObject);
        }
    }

    /** BUTTON EVENT */
    public void BTN_EndTurn() {
        _player.endTurn();
    }

}

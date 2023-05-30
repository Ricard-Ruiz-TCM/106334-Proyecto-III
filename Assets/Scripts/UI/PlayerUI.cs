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

        ManualActor.onSkillUsed += disableSkills;

        Actor.onStepReached += (Node n) => { updateSteps(); };

        Actor.onStartMovement += disableEndTurnButton;
        Actor.onStartAct += disableEndTurnButton;

        Actor.onEndMovement += enableEndTurnButton;
        Actor.onEndAct += disableEndTurnButton;

        BasicActor.onChangeHealth += updateHealth;

        SkillManager.onSkillUsed += updateSingleSkill;

        TurnManager.instance.onStartTurn += getPlayer;
        TurnManager.instance.onModifyAttenders += getPlayer;
    }

    // Unity OnDisable
    private void OnDisable() {
        BuffManager.onApplyBuff -= displayBuffs;
        BuffManager.onRemoveBuff -= displayBuffs;

        ManualActor.onSkillUsed -= disableSkills;

        Actor.onStepReached -= (Node n) => { updateSteps(); };

        Actor.onStartMovement += disableEndTurnButton;
        Actor.onStartAct += disableEndTurnButton;

        Actor.onEndMovement -= enableEndTurnButton;
        Actor.onEndAct -= enableEndTurnButton;

        BasicActor.onChangeHealth -= updateHealth;

        SkillManager.onSkillUsed -= updateSingleSkill;

        TurnManager.instance.onStartTurn -= getPlayer;
        TurnManager.instance.onModifyAttenders -= getPlayer;
    }

    /** M�todo que asigna el player al sistema, para los posibles jugadores */
    private void getPlayer() {
        Turnable turnable = TurnManager.instance.current;
        if (turnable is StaticActor)
            return;

        Actor actor = (Actor)turnable;

        // Nos vamos si hemos muertoo, desactivando
        if (actor == null) {
            return;
        }

        disableSkills();
        _btnEndTurn.interactable = false;

        // Set del player
        if (actor is ManualActor) {
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
        if (_player == null)
            return;

        _defense.UpdateText(_player.totalDefense());
    }

    /** Enable and siable EndTurnButton */
    private void disableEndTurnButton() {
        _btnEndTurn.interactable = false;
    }
    private void enableEndTurnButton() {
        _btnEndTurn.interactable = true;
        updateSteps();
    }

    /** M�todo que actualiz ael hud de vida */
    private void updateHealth() {
        if (_player == null)
            return;

        _imgHealth.fillAmount = (float)((float)(_player).health() / (float)(_player).maxHealth());
    }

    /** M�todo que acutaliza los steps */
    private void updateSteps() {
        if (_player == null)
            return;

        _steps.UpdateText(0);
        if (!_player.isMovementDone())
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
        clearPanel(_panelSkills);
        foreach (SkillItem skI in _player.skills.skills) {
            GameObject btn = GameObject.Instantiate(_skillButtonPfb, _panelSkills);
            _skillButtons.Add(skI.skill.ID, btn.GetComponent<SkillButtonUI>());
            btn.GetComponent<SkillButtonUI>().Set(_player, skI, (KeyCode)(((int)KeyCode.Alpha0) + i), i);
            i++;
            if (skI.skill.needWeapon && !_player.CanAttack()) {
                btn.GetComponent<Button>().interactable = false;
            }
        }

        _btnEndTurn.interactable = (_player.CompareTag("Player"));
    }

    /** Método para desabilitar los skills icons */
    private void disableSkills() {
        foreach (KeyValuePair<skillID, SkillButtonUI> entry in _skillButtons) {
            entry.Value.gameObject.GetComponent<Button>().interactable = false;
        }
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

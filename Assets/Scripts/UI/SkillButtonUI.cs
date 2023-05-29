using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillButtonUI : MonoBehaviour {

    /** Skill using */
    private Skill _skill;
    private SkillItem _skillItem;
    public SkillItem SkItem => _skillItem;

    [SerializeField]
    private UIText _txtCooldown, _txtShortcut;

    /** Keyboard Shortcut */
    private KeyCode _shortcut;

    /** Skill Action */
    private Action _action = null;

    /** Skill Button */
    private Button _btn;

    private void Awake() {
        _btn = GetComponent<Button>();
    }

    public void UpdateCooldown() {
        _txtCooldown.UpdateText(_skillItem.cooldown > 0 ? _skillItem.cooldown.ToString() : "");
        _btn.interactable = (_skillItem.cooldown <= 0);
    }

    public void Set(Turnable actor, SkillItem skill, KeyCode shortcut, int pos) {
        _skillItem = skill;
        _skill = skill.skill;
        _shortcut = shortcut;
        _btn.image.sprite = _skill.icon;
        _txtShortcut.UpdateText(pos.ToString());
        UpdateCooldown();
        if (skill.skill.personal) {
            _action = () => {
                ((Actor)actor).skills.useSkill(_skill.ID, (Actor)actor);
            };
        } else {
            _action = () => {
                ((ManualActor)TurnManager.instance.current).setSkillToUse(_skill.ID);
                TurnManager.instance.current.startAct();
            };
        }
        _btn.onClick.AddListener(() => { _action(); });
    }

    // Unity Update
    void Update() {
        if (_btn.interactable) {
            if (Input.GetKeyDown(_shortcut)) {
                _action();
            }
        }
    }

}

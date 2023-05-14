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
    private Action _action; 

    /** Skill Button */
    private Button _btn;

    private void Awake() {
        _btn = GetComponent<Button>();
    }

    public void Set(Actor actor, SkillItem skill, KeyCode shortcut, int pos) {
        _skillItem = skill;
        _skill = skill.skill;
        _shortcut = shortcut;
        _btn.image.sprite = _skill.icon;
        _txtShortcut.UpdateText(pos.ToString());
        _txtCooldown.UpdateText(skill.cooldown > 0 ? skill.cooldown.ToString() : "");
        _action = () => { actor.GetComponent<ActorSkills>().UseSkill(skill.skill.skill); };
        _btn.onClick.AddListener(() => { _action(); });
    }

    // Unity Update
    void Update() {
        if (_btn.interactable) {
            if (uCore.Action.GetKeyDown(_shortcut)) {
                _action();
            }
        }
    }

}

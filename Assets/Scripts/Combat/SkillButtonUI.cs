using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillButtonUI : MonoBehaviour
{

    private Skill _skill;

    [SerializeField]
    private UIText _cooldownTxt;

    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    public void Set(Actor actor, SkillItem skill)
    {
        _skill = skill.skill;
        _btn.image.sprite = _skill._icon;
        _cooldownTxt.UpdateText(skill.cooldown);
        _btn.onClick.AddListener(() => { actor.UseSkill(skill.skill._skill); });
    }

}

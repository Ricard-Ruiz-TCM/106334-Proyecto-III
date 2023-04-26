using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillButtonUI : MonoBehaviour {

    [SerializeField]
    private Skill _skill;

    private Button _btn;

    private void Awake() {
        _btn = GetComponent<Button>();
    }

    public void Set(Skill skill) {
        _skill = skill;
        _btn.image.sprite = _skill._icon;
        _btn.onClick.AddListener(() => _skill.Special());
    }

}

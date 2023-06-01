using UnityEngine;

public class SkillButtonInfoUI : MonoBehaviour {

    [SerializeField]
    private UIText _txtName, _txtDesc;

    public void Set(SkillItem skill) {
        _txtName.Clear(); 
        //_txtDesc.Clear();
        if (skill != null) {
            _txtName.SetKey(skill.skill.keyName);
            //_txtDesc.SetKey(skill.skill.keyDesc);
        } else {
            gameObject.SetActive(false);
        }
    }

}

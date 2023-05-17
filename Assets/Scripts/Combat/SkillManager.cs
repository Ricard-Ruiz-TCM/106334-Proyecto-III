using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {

    public List<SkillItem> skills;

    public void updateCooldown() {
        foreach (SkillItem si in skills) {
            si.cooldown = Mathf.Clamp(si.cooldown--, 0, si.skill.cooldown);
        }
    }

    public void removeSkill(skillID id) {
        int pos = findSkill(id);

        if (pos != -1) {
            skills.RemoveAt(pos);
        }
    }

    public void addSkill(skillID id) {
        if (findSkill(id) != -1) {
            skills.Add(new SkillItem(uCore.GameManager.GetSkill(id)));
        }
    }

    public bool haveSkill(skillID id) {
        return (findSkill(id) != -1);
    }

    private int findSkill(skillID id) {
        for (int i = 0; i < skills.Count; i++) {
            if (skills[i].skill.ID.Equals(id)) {
                return i;
            }
        }

        return -1;
    }

}
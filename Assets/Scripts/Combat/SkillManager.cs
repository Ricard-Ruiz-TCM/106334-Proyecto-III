using System;
using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {

    public static event Action<skillID> onSkillUsed;

    public List<SkillItem> skills;

    public void updateCooldown() {
        foreach (SkillItem si in skills) {
            si.cooldown = Mathf.Clamp(si.cooldown - 1, -1, si.skill.cooldown);
        }
    }

    public Skill getSkill(skillID id) {
        int pos = findSkill(id);

        if (pos != -1) {
            return skills[pos].skill;
        }

        return null;
    }

    public bool canUse(skillID id) {
        return skills[findSkill(id)].cooldown < 0;
    }

    public void useSkill(skillID id, BasicActor from, BasicActor to = null) {
        int pos = findSkill(id);
        if (pos == -1)
            return;

        if (skills[pos].cooldown < 0) {
            skills[pos].cooldown = skills[pos].skill.cooldown;
            skills[pos].skill.action(from, to);
            onSkillUsed?.Invoke(skills[pos].skill.ID);
        }

    }

    public void removeSkill(skillID id) {
        int pos = findSkill(id);

        if (pos != -1) {
            skills.RemoveAt(pos);
        }
    }

    public void addSkill(skillID id) {
        if (findSkill(id) == -1) {
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
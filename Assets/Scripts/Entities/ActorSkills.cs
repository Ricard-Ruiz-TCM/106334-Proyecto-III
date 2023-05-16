using System.Collections.Generic;
using UnityEngine;


public class ActorSkills : ActorManager {

    [SerializeField, Header("Skills avaliable:")]
    protected List<SkillItem> _skills;

    public void AddSkill(skillID skill) {
        bool already = false;

        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill.ID.Equals(skill))
                already = true;
        }

        if (!already) {
            Skill sk = uCore.GameManager.GetSkill(skill);
            _skills.Add(new SkillItem() { skill = sk, cooldown = sk.cooldown });
            ;
        }
    }
    public void RemoveSkill(skillID skill) {
        if (HaveSkill(skill)) {
            int pos = -1;
            for (int i = 0; i < _skills.Count; i++) {
                if (_skills[i].skill.Equals(skill)) {
                    pos = i;
                    break;
                }
            }
            if (pos != -1) {
                _skills.RemoveAt(pos);
            }
        }
    }
    public void UseSkill(skillID skill) {
        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill.ID.Equals(skill)) {
                if (skillItem.cooldown <= 0) {
                    skillItem.skill.Action(attender);
                    skillItem.cooldown = skillItem.skill.cooldown;
                }
            }
        }
    }
    public void UpdateSkillCooldown() {
        foreach (SkillItem skillItem in _skills) {
            if (skillItem.cooldown > 0) {
                skillItem.cooldown--;
            }
        }
    }
    public bool HaveSkill(skillID skill) {
        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill.ID.Equals(skill))
                return true;
        }
        return false;
    }
    public Skill ReturnSkill(skillID skill)
    {
        foreach (SkillItem skillItem in _skills)
        {
            if (skillItem.skill.ID.Equals(skill))
                return skillItem.skill;
        }
        return null;
    }

    public List<SkillItem> Skills() {
        return _skills;
    }
}


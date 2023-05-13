using System.Collections.Generic;
using UnityEngine;


public class ActorSkills : ActorManager {

    [SerializeField, Header("Skills avaliable:")]
    protected List<SkillItem> _skills;

    public void AddSkill(skills skill) {
        bool already = false;

        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill.skill.Equals(skill))
                already = true;
        }

        if (!already) {
            Skill sk = uCore.GameManager.GetSkill(skill);
            _skills.Add(new SkillItem() { skill = sk, cooldown = sk.cooldown });
            ;
        }
    }
    public void RemoveSkill(skills skill) {
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
    public void UseSkill(skills skill) {
        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill.skill.Equals(skill)) {
                if (skillItem.cooldown <= 0) {
                    skillItem.skill.Special(actor);
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
    public bool HaveSkill(skills skill) {
        foreach (SkillItem skillItem in _skills) {
            if (skillItem.skill.skill.Equals(skill))
                return true;
        }
        return false;
    }
    public Skill ReturnSkill(skills skill)
    {
        foreach (SkillItem skillItem in _skills)
        {
            if (skillItem.skill.skill.Equals(skill))
                return skillItem.skill;
        }
        return null;
    }

    public List<SkillItem> Skills() {
        return _skills;
    }
}


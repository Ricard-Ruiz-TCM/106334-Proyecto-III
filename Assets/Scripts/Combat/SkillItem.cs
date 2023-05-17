using System;

[Serializable]
public class SkillItem {

    public Skill skill;
    public int cooldown;

    public SkillItem(Skill data) {
        skill = data;
        cooldown = data.cooldown;
    }

}

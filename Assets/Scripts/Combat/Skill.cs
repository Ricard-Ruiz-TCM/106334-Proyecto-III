using UnityEngine;

public abstract class Skill : ScriptableObject {

    public Sprite icon;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDesc;

    [Header("Skill:")]
    public skills skill;
    public int cooldown;

    public int duration;

    public int range;

    public abstract void Special(Actor from);

}

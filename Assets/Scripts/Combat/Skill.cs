using UnityEngine;

public abstract class Skill : ScriptableObject {

    public Sprite _icon;

    [Header("Localization Keys:")]
    public string _keyName;
    public string _keyDescription;

    [Header("Skill:")]
    public skills _skill;
    public int _cooldown;

    public int _range;

    [Header("Stats Modifiers:")]
    public int _damageMod;
    public int _defenseMod;

    public abstract void Special(Actor from);

}

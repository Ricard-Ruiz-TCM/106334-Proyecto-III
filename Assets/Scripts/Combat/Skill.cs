using UnityEngine;

public abstract class Skill : ScriptableObject {

    public Sprite _icon;

    [Header("Localization Keys:")]
    public string _keyName;
    public string _keyDescription;

    [Header("Skill:")]
    public skills _skill;
    public int _cooldown;

    [Header("Stats Modifiers:")]
    public float _damageMod;
    public float _defenseMod;

    public abstract void Special();

}

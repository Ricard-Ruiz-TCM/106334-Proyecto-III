using UnityEngine;

public abstract class Skill : ScriptableObject {

    public skills _skill;
    public int _cooldown;

    [Header("Stats Modifiers:")]
    public float _damageMod;
    public float _defenseMod;

    public abstract void Special();

}

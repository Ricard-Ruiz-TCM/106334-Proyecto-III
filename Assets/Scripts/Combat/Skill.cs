using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

    public float _cooldown;

    [Header("Stats Modifiers:")]
    public float _damageMod;
    public float _defenseMod;

    public abstract void Special();

}

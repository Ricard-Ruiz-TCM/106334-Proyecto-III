using UnityEngine;

public abstract class Status : ScriptableObject {

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDesc;

    [Header("Status:")]
    public aStatus status;
    public int duration;

    public abstract void Effect(Actor me);

}

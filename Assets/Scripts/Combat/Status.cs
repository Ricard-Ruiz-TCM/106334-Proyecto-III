using UnityEngine;

public abstract class Status : ScriptableObject {

    public Sprite icon;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDesc;

    [Header("Status:")]
    public buffsnDebuffs status;
    public int duration;

    public abstract void Effect(Actor me);

}

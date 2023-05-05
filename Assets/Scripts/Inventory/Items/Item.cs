using UnityEngine;

public abstract class Item : ScriptableObject {

    [Header("Core:")]
    public items item;
    public Sprite icon;

    public float weight;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDescription;

}

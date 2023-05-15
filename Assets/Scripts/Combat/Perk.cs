using UnityEngine;

public abstract class Perk : ScriptableObject {

    [Header("Sprite Icon:")]
    public Sprite icon;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDesc;

    [Header("Data:")]
    public perkID ID;
    public modType modType;

    [Header("Dependency:")]
    public perkID dependency;

}

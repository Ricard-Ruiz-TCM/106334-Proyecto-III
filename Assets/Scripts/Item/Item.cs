using UnityEngine;

public abstract class Item : ScriptableObject {

    [Header("Icon:")]
    public Sprite icon;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDescription;

    [Header("Data:")]
    public itemID ID;

}

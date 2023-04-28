using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Items/Item")]
public class Item : ScriptableObject {

    [Header("Core:")]
    public items item;
    public Sprite icon;

    public float weight;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDescription;

}

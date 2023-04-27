using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{

    public items _item;
    public Sprite _icon;

    public float _weight;

    [Header("Localization Keys:")]
    public string _keyName;
    public string _keyDescription;

}

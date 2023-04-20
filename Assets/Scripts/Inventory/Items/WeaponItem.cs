using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponItem", menuName = "Items/Weapon Item")]
public class WeaponItem : Item {

    [Header("Weapon Stats:")]
    public float _damage;
    public float _range;

    public Skill _special;

}

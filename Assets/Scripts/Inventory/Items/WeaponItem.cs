using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponItem", menuName = "Items/Weapon Item")]
public class WeaponItem : Item {

    [Header("Weapon Stats:")]
    public int[] _damage;
    public int _range;

    public Skill _skill;

}

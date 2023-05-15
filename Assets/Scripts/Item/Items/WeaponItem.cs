using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponItem", menuName = "Items/Weapon Item")]
public class WeaponItem : Item {

    [Header("Weapon Stats:")]
    public int range;
    public int[] damage;

    [Header("Special Skill:")]
    public skillID skill;

}
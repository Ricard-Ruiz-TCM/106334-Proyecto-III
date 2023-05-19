using UnityEngine;

[CreateAssetMenu(fileName = "new ShieldItem", menuName = "Items/Shield Item")]
public class ShieldItem : Item {

    [Header("Shield Stats:")]
    public int[] defense;
    public int movement;

    public Skill skill;

}

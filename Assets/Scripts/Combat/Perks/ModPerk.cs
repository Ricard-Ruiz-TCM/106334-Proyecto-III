using UnityEngine;

[CreateAssetMenu(fileName = "new ModPerk", menuName = "Combat/Perks/Mod Perk")]
public class ModPerk : Perk {

    [Header("Effect on:")]
    public modType type;
    [Header("Value:")]
    public int value;
    [Header("How affect:")]
    public modOperation operation;

}

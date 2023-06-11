using UnityEngine;

[CreateAssetMenu(fileName = "new ModPerk", menuName = "Combat/Perks/Mod Perk")]
public class ModPerk : Perk {

    [Header("Effect on:")]
    public modType type;
    [Header("Value:")]
    public float value;
    [Header("How affect:")]
    public modOperation operation;

    public int apply(float inputValue) {
        switch (operation) {
            case modOperation.add:
                return (int)(inputValue + value);
            case modOperation.sub:
                return (int)(inputValue - value);
            case modOperation.mult:
                return (int)(inputValue * value);
            case modOperation.div:
                return (int)(inputValue * value);
        }
        return (int)inputValue;
    }

}

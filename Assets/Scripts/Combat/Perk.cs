using UnityEngine;

public abstract class Perk : ScriptableObject {

    public perks perk;
    public perkCategory category;
    public modificationType modificationType;

    [Header("Dependency:")]
    public perks dependency;

}

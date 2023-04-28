using UnityEngine;

public abstract class Perk : ScriptableObject {

    public perks perk;
    public perkCategory category;
    public perkModification modificationType;

    [Header("Dependency:")]
    public perks dependency;

}

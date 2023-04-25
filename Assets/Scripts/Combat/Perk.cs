using UnityEngine;

public abstract class Perk : ScriptableObject {

    public perks _perk;
    public perkCategory _category;
    public perkModification _modificationType;

    [Header ("Dependency:")]
    public perks _dependency;


}

using UnityEngine;

public abstract class Perks : ScriptableObject {

    public int _id;

    public perkCategory _category;
    public perkModification _modificationType;

    public int _dependency;


}

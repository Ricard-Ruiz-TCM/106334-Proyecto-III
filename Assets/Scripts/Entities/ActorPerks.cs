using System.Collections.Generic;
using UnityEngine;


public class ActorPerks : ActorManager {

    [SerializeField, Header("Acive Perks:")]
    protected List<Perk> _perks;
    public List<Perk> Perks() {
        return _perks;
    }

    public void AddPerk(Perk perk) {

    }

    public void RemovePerk(Perk perk) {

    }

    public bool HavePerk(Perk perk) {
        foreach (Perk pks in _perks) {
            if (pks.Equals(perk))
                return true;
        }
        return false;
    }

    SOBox<perkID> _perkss;

}

using UnityEngine;
using System.Collections.Generic;

public class PerkManager : MonoBehaviour {

    public List<PerkItem> perks;

    public void removePerk(perkID id) {
        int pos = findPerk(id);

        if (pos != -1) {
            perks.RemoveAt(pos);
        }
    }

    public void addSkill(perkID id) {
        if (findPerk(id) == -1) {
            perks.Add(new PerkItem(uCore.GameManager.GetPerk(id)));
        }
    }

    public bool haveSkill(perkID id) {
        return (findPerk(id) != -1);
    }

    private int findPerk(perkID id) {
        for (int i = 0; i < perks.Count; i++) {
            if (perks[i].perk.ID.Equals(id)) {
                return i;
            }
        }

        return -1;
    }

}
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour {

    public List<BuffItem> activeBuffs;

    public void updateBuffs() {
        foreach (BuffItem bi in activeBuffs) {
            bi.duration--;
            if (bi.duration <= 0) {
                removeBuff(bi.buff.ID);
            }
        }
    }

    public void removeBuff(buffsID id) {
        int pos = findBuff(id);

        if (pos != -1) {
            activeBuffs.RemoveAt(pos);
        }
    }

    public void applyBuff(Actor actor, buffsID id) {
        if (findBuff(id) != -1) {
            activeBuffs.Add(new BuffItem(uCore.GameManager.GetBuff(id)));
            activeBuffs[activeBuffs.Count - 1].buff.onApply(actor);
        }
    }

    public bool isBuffActive(buffsID id) {
        return (findBuff(id) != -1);
    }

    private int findBuff(buffsID id) {
        for (int i = 0; i < activeBuffs.Count; i++) {
            if (activeBuffs[i].buff.ID.Equals(id)) {
                return i;
            }
        }

        return -1;
    }

}

using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour {

    public List<BuffItem> activeBuffs;

    public void updateBuffs(BasicActor actor) {
        for (int i = 0; i < activeBuffs.Count; i++) {
            activeBuffs[i].duration--;
            if (activeBuffs[i].duration <= 0) {
                activeBuffs[i].buff.onRemove(actor);
                removeBuff(activeBuffs[i].buff.ID);
                i--;
            }
        }
    }

    public void applyStartTurnEffect(BasicActor actor) {
        foreach(BuffItem bi in activeBuffs) {
            bi.buff.startTurnEffect(actor);
        }
    }

    public void applyEndTurnEffect(BasicActor actor) {
        foreach (BuffItem bi in activeBuffs) {
            bi.buff.endTurnEffect(actor);
        }
    }

    public void removeBuff(buffsID id) {
        int pos = findBuff(id);

        if (pos != -1) {
            activeBuffs.RemoveAt(pos);
        }
    }

    public void applyBuffs(Actor actor, params buffsID[] ids) {
        foreach (buffsID id in ids) {
            if (findBuff(id) == -1) {
                activeBuffs.Add(new BuffItem(uCore.GameManager.GetBuff(id)));
                activeBuffs[activeBuffs.Count - 1].buff.onApply(actor);
            }
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

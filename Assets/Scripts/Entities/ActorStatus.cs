using System.Collections.Generic;
using UnityEngine;

public class ActorStatus : ActorManager {

    [SerializeField, Header("Stats:")]
    protected List<BuffItem> _status;
    public List<BuffItem> ActiveStatus => _status;

    public bool isStatusActive(buffsID status) {
        foreach (BuffItem statusItem in _status) {
            if (statusItem.buff.ID.Equals(status)) {
                return true;
            }
        }

        return false;
    }

    public void ApplyStatus(buffsID status) {
        bool already = false;

        foreach (BuffItem statusItem in _status) {
            if (statusItem.buff.ID.Equals(status)) {
                already = true;
                statusItem.duration = uCore.GameManager.GetBuff(status).duration;
            }
        }

        if (!already) {
            Buff st = uCore.GameManager.GetBuff(status);
            _status.Add(new BuffItem() { buff = st, duration = st.duration });
        }
    }

    public void UpdateStatus() {
        foreach (BuffItem status in _status) {
            if (status.duration <= 0) {
                _status.Remove(status);
            }
        }
    }

    public void StatusEffect() {
        foreach (BuffItem effect in _status) {
            effect.buff.endTurnEffect(actor);
            effect.duration--;
        }
    }

}

using System.Collections.Generic;
using UnityEngine;

public class ActorStatus : ActorManager {

    [SerializeField, Header("Stats:")]
    protected List<StatusItem> _status;

    public bool isStatusActive(buffsnDebuffs status) {
        foreach (StatusItem statusItem in _status) {
            if (statusItem.status.status.Equals(status)) {
                return true;
            }
        }

        return false;
    }

    public void ApplyStatus(buffsnDebuffs status) {
        bool already = false;

        foreach (StatusItem statusItem in _status) {
            if (statusItem.status.status.Equals(status)) {
                already = true;
                statusItem.duration = uCore.GameManager.GetStatus(status).duration;
            }
        }

        if (!already) {
            Status st = uCore.GameManager.GetStatus(status);
            _status.Add(new StatusItem() { status = st, duration = st.duration });
        } 
    }

    public void UpdateStatus() {
        foreach (StatusItem status in _status) {
            if (status.duration <= 0) {
                _status.Remove(status);
            }
        }
    }

    public void StatusEffect() {
        foreach (StatusItem effect in _status) {
            effect.status.Effect(actor);
            effect.duration--;
        }
    }

}

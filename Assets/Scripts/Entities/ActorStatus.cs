using System.Collections.Generic;
using UnityEngine;

public class ActorStatus : MonoBehaviour {

    private Actor _actor = null;
    [HideInInspector]
    public Actor actor {
        get {
            if (_actor == null)
                _actor = GetComponent<Actor>();

            return _actor;
        }
        set {
        }
    }

    [SerializeField, Header("Stats:")]
    protected List<StatusItem> _status;

    public void ApplyStatus(aStatus status) {
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

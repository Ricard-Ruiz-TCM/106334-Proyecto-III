using UnityEngine;

public abstract class ActorManager : MonoBehaviour {

    // Actor getter
    private Turnable _attender = null;
    [HideInInspector]
    public Turnable attender {
        get {
            if (_attender == null)
                _attender = GetComponent<Turnable>();

            return _attender;
        }
        set {
        }
    }

}

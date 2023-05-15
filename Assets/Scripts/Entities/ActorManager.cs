using UnityEngine;

public abstract class ActorManager : MonoBehaviour {

    // Actor getter
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

}

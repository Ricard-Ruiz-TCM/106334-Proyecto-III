using UnityEngine;
using UnityEngine.Rendering;

public class EffectElement : BasicElement<EffectElement> {

    private effects _type;
    public EffectElement Set(effects type, float duration = float.PositiveInfinity) {
        _duration = duration;
        _type = type; 
        return this;
    }
    public effects Type() { return _type; }

    public T Get<T>() where T : VolumeComponent {
        T fx = null;
        EffectProfile.TryGet<T>(out fx);
        return fx;
    }

    public VolumeProfile EffectProfile {
        get {
            return GetComponent<Volume>().profile;
        }
    }

    public float Duration => _duration;
    private float _duration = float.PositiveInfinity;

    private GameObject _camera = null;
    public GameObject Camera {
        get {
            if (_camera == null)
                _camera = UnityEngine.Camera.main.gameObject;

            return _camera;
        }
        set {
            _camera = value;
        }
    }

    public override EffectElement destroyoAtEnd() {
        destroyOnTime(Duration);
        return this;
    }
}

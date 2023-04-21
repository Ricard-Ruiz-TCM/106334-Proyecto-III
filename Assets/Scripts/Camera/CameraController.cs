using UnityEngine;

public class CameraController : MonoBehaviour {

    // Target que vamos a "seguir"
    [SerializeField, Header("Target:")]
    private Transform _target;

    [SerializeField, Header("Zoom:")]
    private float _maxZoom;
    [SerializeField]
    private float _minZoom;

    [SerializeField, Header("Position:")]
    private Vector2 _offset;
    public void SetOffset(Vector2 offset) {
        _offset = offset;
    }
    [SerializeField]
    private Vector2 _limits;
    public void SetLimits(Vector2 limits) {
        _limits = limits;
    }

    // Unity LateUpdate
    void LateUpdate() {

    }

}

using UnityEngine;

public class CameraGPT : MonoBehaviour {


    [SerializeField, Header("Active:")]
    private bool _active;

    [SerializeField, Header("Target:")]
    private Transform _target;
    [SerializeField]
    private float _cameraYTargetOffset = 3f;

    [SerializeField, Header("Limits:")]
    private limit _zoomLimit;
    [SerializeField]
    private limit _xRotLimit;

    [SerializeField, Header("Speeds:")]
    private float _speed = 5f;
    [SerializeField]
    private float _zoomSpeed = 5f;
    [SerializeField]
    private float _rotSpeed = 5f;
    [SerializeField]
    private float _rotArountSpeed = 100f;

    [SerializeField, Header("Distancia:")]
    private float _distance;

    /** Vectroes para pos y rot temporales */
    private Vector3 _targetRot;
    private Vector3 _targetPos;

    /** La última en zoom que teniamos antes de ataque */
    private float _lastY;

    // Unity OnEnable
    void OnEnable() {
        Actor.onReAct += restoreZoom;
        Actor.onEndAct += restoreZoom;
        Actor.onStartAct += zoomOut;

        TurnManager.instance.onStartSystem += activate;
        TurnManager.instance.onNewCurrentTurnable += setTarget;
    }

    // Unity OnDisabel
    void OnDisable() {
        Actor.onReAct -= restoreZoom;
        Actor.onEndAct -= restoreZoom;
        Actor.onStartAct -= zoomOut;

        TurnManager.instance.onStartSystem -= activate;
        TurnManager.instance.onNewCurrentTurnable -= setTarget;
    }

    // Unity Start
    void Start() {
        _targetPos.y = _zoomLimit.max;    
    }

    // Unity FixedUpdate
    void FixedUpdate() {
        if (!_active)
            return;

        // Behaviours
        cameraZoom();
        cameraRotation();
        cameraFollow();
        cameraLookAt();

        // Clamp
        cameraClamp();

        // Suavizado del movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _targetRot, _rotSpeed * Time.deltaTime);

    }

    /** Método para setear el target */
    public void setTarget(Turnable target) {
        _target = target.transform;
    }

    /** Método para activar la cámara */
    public void activate() {
        _active = true;
    }

    /** Método que levanta la camara like "zoom" */
    private void cameraZoom() {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        _targetPos += Vector3.down * scrollAmount * _zoomSpeed;
    }

    /** Método para rotar la camara desde el eje central*/
    private void cameraRotation() {
        if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftAlt)) {
            float mouseX = Input.GetAxis("Mouse X");
            transform.RotateAround(_target.transform.position, new Vector3(0f, mouseX, 0f), _rotArountSpeed * Time.deltaTime);
        }
    }

    /** Método para hacer lookAt de la camara */
    private void cameraLookAt() {
        Vector3 posTarget = _target.transform.position;
        posTarget.y += _cameraYTargetOffset;
        Vector3 direction = posTarget - transform.position;
        _targetRot = Quaternion.LookRotation(direction).eulerAngles;
    }

    /** Método para la camara, que siempre siga al target */
    private void cameraFollow() {
        Vector3 currentPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetPosition = new Vector3(_target.position.x, 0f, _target.position.z);
        float distance = Vector3.Distance(currentPosition, targetPosition);
        if (distance > _distance) {
            Vector3 direction = (targetPosition - currentPosition).normalized;
            _targetPos.x = _target.position.x - direction.x * _distance;
            _targetPos.z = _target.position.z - direction.z * _distance;
        }
    }

    /** Clamps para posicion y rotaicón */
    private void cameraClamp() {
        _targetPos.y = Mathf.Clamp(_targetPos.y, _zoomLimit.min, _zoomLimit.max);
        _targetRot.x = Mathf.Clamp(_targetRot.x, _xRotLimit.min, _xRotLimit.max);
    }

    /** Métodos para hacer zoom a la haora de realizar un ataque */
    private void zoomOut() {
        _lastY = transform.position.y;
        _targetPos.y = _zoomLimit.max;
    }
    private void restoreZoom() {
        _targetPos.y = _lastY;
    }

}


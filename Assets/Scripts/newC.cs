using System.Collections;
using UnityEngine;

public class newC : MonoBehaviour {

    [SerializeField]
    private Camera _camera;

    [SerializeField, Header("Active:")]
    private bool _active;

    [SerializeField, Header("Limits:")]
    private limit _zoomLimit;
    [SerializeField]
    private limit _verticalRotLimit;

    [SerializeField, Header("Speeds:")]
    private float _zoomSpeed;
    [SerializeField]
    private float _rootSpeed;
    [SerializeField] float breathFovSpeed;

    [SerializeField, Header("Smooths:")]
    private float _rotSmoothTime;
    [SerializeField]
    private float _moveSmoothTime;
    private float _moveSavageSmoothTime;
    private float _moveSlowTime;

    [SerializeField, Header("Distancia:")]
    public float _distance;

    [Header("Positions:")]
    public Transform _positioningPosition;


    [SerializeField, Header("Center Point:")]
    private Transform _pivot;

    // Working Rot & Post
    private Vector3 _targetRot = Vector3.zero;
    private Vector3 _rotSmoothSpeed = Vector3.zero;
    [SerializeField] float hasTouchedSpeed = 3;
    bool touchingWall = false;

    public float _breathSpeed = 1.0f; // Adjust the speed of the breathing animation
    public float _breathMagnitude = 0.1f; // Adjust the magnitude of the breathing animation
    float _innitialFOV;

    // Coroutines
    private Coroutine _animThinking;

    // Unity OnEnable
    void OnEnable() {
        TurnManager.onNewRound += startRound;
        TurnManager.onEndRound += endRound;

        TurnManager.onEndTurn += () => { _moveSmoothTime = _moveSlowTime; };
        TurnManager.onStartTurn += () => { _moveSmoothTime = _moveSavageSmoothTime; };
    }

    // Unity OnDisabel
    void OnDisable() {
        TurnManager.onNewRound -= startRound;
        TurnManager.onEndRound -= endRound;

        TurnManager.onEndTurn -= () => { _moveSmoothTime = _moveSlowTime; };
        TurnManager.onStartTurn -= () => { _moveSmoothTime = _moveSavageSmoothTime; };
    }

    private void Start() {
        _innitialFOV = _camera.fieldOfView;
        _moveSlowTime = _moveSmoothTime;
        _moveSavageSmoothTime = _moveSlowTime * 10000f;
        _animThinking = StartCoroutine(AnimThinking());
    }

    // Unity FixedUpdate
    void FixedUpdate() {
        if (!_active)
            return;

        if (!touchingWall) {
            cameraZoom();
        }

        cameraRotation();

        // Clamps
        _distance = Mathf.Clamp(_distance, _zoomLimit.min, _zoomLimit.max);
        _targetRot.x = Mathf.Clamp(_targetRot.x, _verticalRotLimit.min, _verticalRotLimit.max);

        // Update de la posición
        Vector3 targetPos = _pivot.position - transform.forward * _distance;
        transform.position = Vector3.Lerp(transform.position, targetPos, _moveSmoothTime);

        //Update de la rotación
        transform.localEulerAngles = _targetRot;
    }

    /** Método para activar la cámara */
    public void activate() {
        _active = true;
    }

    /** Método que levanta la camara like "zoom"  DOOOONE*/
    private void cameraZoom() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _distance = Mathf.Lerp(_distance, _distance - scroll * _zoomSpeed, _rotSmoothTime);
    }

    /** Método para rotar la camara desde el eje central  DOOOONE*/
    private void cameraRotation() {
        float mouseX = 0f, mouseY = 0f;
        if ((Input.GetMouseButton(2)) && (_moveSmoothTime > _moveSlowTime)) {
            mouseX = -Input.GetAxis("Mouse X") * _rootSpeed;
            mouseY = -Input.GetAxis("Mouse Y") * _rootSpeed;
        }
        Vector3 nextRot = new Vector3(transform.localEulerAngles.x + mouseY, transform.localEulerAngles.y - mouseX);
        _targetRot = Vector3.SmoothDamp(transform.localEulerAngles, nextRot, ref _rotSmoothSpeed, _rotSmoothTime);
    }

    public void startRound(roundType round) {
        switch (round) {
            case roundType.positioning:
                StopCoroutine(_animThinking);
                StartCoroutine(StartAnim());
                break;
        }
    }

    public void endRound(roundType round) {
        switch (round) {
            case roundType.positioning:
                StopAllCoroutines();
                transform.position = _positioningPosition.position;
                transform.rotation = _positioningPosition.rotation;
                _camera.fieldOfView = _innitialFOV;
                _camera.orthographic = false;
                activate();
                break;
            case roundType.thinking:
                break;

        }
    }

    /** Start Anim to move 2 the positioning round */
    private IEnumerator StartAnim() {
        float timer = 0, duration = 1.5f;

        Vector3 innitialPos = transform.position;
        Vector3 innitialRot = transform.eulerAngles;

        float percentageDuration = 0f;
        while (percentageDuration <= 1f) {
            timer += Time.deltaTime; 
            percentageDuration = timer / duration;
            transform.position = Vector3.Lerp(innitialPos, _positioningPosition.position, percentageDuration);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(innitialRot), Quaternion.Euler(_positioningPosition.localEulerAngles), percentageDuration);
            yield return null;
        }

        _camera.orthographic = true;
        transform.position = _positioningPosition.position;
        transform.rotation = _positioningPosition.rotation;
    }

    private IEnumerator AnimThinking() {
        int fovDir = -1;
        while (true) {
            float breath = Mathf.Sin(Time.time * _breathSpeed) * _breathMagnitude;

            // Apply the breathing animation to the local position of the object
            Vector3 newPosition = transform.localPosition + new Vector3(0f, breath, 0f);
            _camera.fieldOfView += breath * breathFovSpeed * fovDir;
            transform.localPosition = newPosition;
            if (Mathf.Abs(_camera.fieldOfView - _innitialFOV) > 1) {
                fovDir *= -1;
            }
            yield return null;
        }
    }

    private IEnumerator AnimAttack() {
        yield return new WaitForSeconds(0.5f);
        _targetRot = new Vector3(25, 180, 0);
        float timer = 0;

        float duration = 3f;
        while (timer < duration) {
            timer += Time.deltaTime;
            float percentageDuration = timer / duration;
            transform.position = Vector3.Lerp(transform.position, _pivot.position - transform.forward * _distance, percentageDuration);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(_targetRot), percentageDuration);
            yield return new WaitForFixedUpdate();
        }

        transform.position = _pivot.position - transform.forward * _distance;
        transform.eulerAngles = _targetRot;
        //activate();
    }

    // Collisions
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("CameraWall")) {
            _distance -= hasTouchedSpeed * Time.deltaTime;
            touchingWall = true;
        }

    }
    private void OnTriggerExit(Collider other) {
        touchingWall = false;
    }

}

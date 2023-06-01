using System.Collections;
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

    [SerializeField, Header("Timings:2")]
    private float _focusTime = 1f;

    /** Vectroes para pos y rot temporales */
    private Quaternion _targetRot;
    private Vector3 _targetLookAtPosition;
    private Vector3 _targetPos;

    private Transform _temporalTarget;

    public Transform _positioningPos;
    public Transform _completedPos;

    /** La última en zoom que teniamos antes de ataque */
    private float _lastY;

    // Unity OnEnable
    void OnEnable() {
        BasicActor.onReAct += restoreZoom;
        BasicActor.onEndAct += restoreZoom;
        BasicActor.onStartAct += zoomOut;

        TurnManager.instance.onNewRound += setPosition;

        Actor.onSkillUsed += focusNode;

        TurnManager.instance.onStartSystem += activate;
        TurnManager.instance.onStartTurn += () => { setTarget(TurnManager.instance.current); };
    }

    // Unity OnDisabel
    void OnDisable() {
        BasicActor.onReAct -= restoreZoom;
        BasicActor.onEndAct -= restoreZoom;
        BasicActor.onStartAct -= zoomOut;

        Actor.onSkillUsed -= focusNode;

        TurnManager.instance.onNewRound -= setPosition;

        TurnManager.instance.onStartSystem -= activate;
        TurnManager.instance.onStartTurn -= () => { setTarget(TurnManager.instance.current); };
    }

    // Unity FixedUpdate
    void FixedUpdate() {
        if (!_active)
            return;

        if (_target != null) {
            // Behaviours
            cameraRotation();
            cameraFollow();
            cameraLookAt();
            // Clamp
            cameraClamp();
        }
        // Zoom
        cameraZoom();

        // Suavizado del movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRot, _rotSpeed * Time.deltaTime);
    }

    /** Método para setear el target */
    public void setTarget(Turnable target) {
        _targetPos.y = 8f;
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
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt)) {
            float mouseX = Input.GetAxis("Mouse X");
            transform.RotateAround(_target.transform.position, Vector3.up, mouseX * _rotArountSpeed * Time.deltaTime);
        }
    }

    /** Método para hacer lookAt de la camara */
    private void cameraLookAt() {
        _targetLookAtPosition = _target.position + Vector3.up * _cameraYTargetOffset;
        _targetRot = Quaternion.LookRotation(_targetLookAtPosition - transform.position);
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
    }

    /** Métodos para hacer zoom a la haora de realizar un ataque */
    private void zoomOut() {
        _lastY = transform.position.y;
        _targetPos.y = _zoomLimit.max - 2f;
    }
    private void restoreZoom() {
        _targetPos.y = _lastY;
    }

    /** Método para establecer el focus temporal */
    private void focusNode(Node target) {
        _temporalTarget = _target;
        _target = Stage.StageBuilder.getGridPlane(target).transform;
        StartCoroutine(CRestoreTarget(_focusTime));
    }

    /** Méotod ede coroutine para volver al foco normal */
    private IEnumerator CRestoreTarget(float delay) {
        yield return new WaitForSeconds(delay);
        _target = _temporalTarget;
    }

    public void setPosition(roundType round) {
        switch (round) {
            case roundType.positioning:
                _targetPos = _positioningPos.position;
                _targetRot = _positioningPos.rotation;
                break;
        }
    }

}


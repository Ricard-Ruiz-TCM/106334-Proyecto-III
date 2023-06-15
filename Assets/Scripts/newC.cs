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

    [SerializeField, Header("Distancia:")]
    private float _distance;

    [Header("Positions:")]
    public Transform _positioningPosition;
    // LIBRE POSITION
    public Transform _completedPosition;


    [SerializeField, Header("Center Point:")]
    private Transform _pivot;

    // Working Rot & Post
    private Vector3 _targetRot = Vector3.zero;
    private Vector3 _rotSmoothSpeed = Vector3.zero;
    [SerializeField] float hasTouchedSpeed = 3;
    bool touchingWall = false;

    public float breathSpeed = 1.0f; // Adjust the speed of the breathing animation
    public float breathMagnitude = 0.1f; // Adjust the magnitude of the breathing animation
    float initialFov;

    // Unity OnEnable
    void OnEnable() {
        TurnManager.onNewRound += startRound;
        TurnManager.onEndRound += endRound;
    }

    // Unity OnDisabel
    void OnDisable() {
        TurnManager.onNewRound -= startRound;
        TurnManager.onEndRound -= endRound;
    }
    private void Start()
    {
        initialFov = _camera.fieldOfView;
        StartCoroutine(AnimThinking());
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

        // Update de la posici�n
        Vector3 targetPos = _pivot.position - transform.forward * _distance;
        transform.position = Vector3.Lerp(transform.position, targetPos, _moveSmoothTime);

        //Update de la rotaci�n
        transform.localEulerAngles = _targetRot;
    }


    private IEnumerator play(Turnable target) {
        yield return null;
        /*if (!canPlay) {
            animating = true;
            yield return new WaitForSeconds(0.5f);
            float timer = 0;
            Vector3 startpos = transform.position;
            Quaternion startRot = transform.rotation;

            while (timer < duration) {
                timer += Time.deltaTime;
                float percentageDuration = timer / duration;
                transform.position = Vector3.Lerp(startpos, playAnimFinalPos, percentageDuration);
                transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(playAnimFinalRot), percentageDuration);
                yield return new WaitForFixedUpdate();
            }

            transform.position = playAnimFinalPos;
            transform.rotation = Quaternion.Euler(playAnimFinalRot);
            animating = false;
            canPlay = true;

            _targetPos.y = 8f;
            _target = target.transform;

        } else {
            _targetPos.y = 8f;
            _target = target.transform;
        }*/
    }

    /** M�todo para activar la c�mara */
    public void activate() {
        _active = true;
    }

    /** M�todo que levanta la camara like "zoom"  DOOOONE*/
    private void cameraZoom() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _distance = Mathf.Lerp(_distance, _distance - scroll * _zoomSpeed, _rotSmoothTime);
    }

    /** M�todo para rotar la camara desde el eje central  DOOOONE*/
    private void cameraRotation() {
        float mouseX = 0f, mouseY = 0f;
        if (Input.GetMouseButton(1)) {
            mouseX = -Input.GetAxis("Mouse X") * _rootSpeed;
            mouseY = -Input.GetAxis("Mouse Y") * _rootSpeed;
        }
        Vector3 nextRot = new Vector3(transform.localEulerAngles.x + mouseY, transform.localEulerAngles.y - mouseX);
        _targetRot = Vector3.SmoothDamp(transform.localEulerAngles, nextRot, ref _rotSmoothSpeed, _rotSmoothTime);
    }

    public void startRound(roundType round) {
        
        switch (round) {
            case roundType.positioning:
                StartCoroutine(StartAnim());
                break;
            case roundType.combat:
                
                //StartCoroutine(AnimAttack());
                break;
        }
    }

    public void endRound(roundType round) {
        switch (round) {
            case roundType.positioning:
                StopAllCoroutines();
                transform.position = _positioningPosition.position;
                transform.rotation = _positioningPosition.rotation;
                activate();
                break;
            case roundType.combat:
                StartCoroutine(EndAnim());
                break;
            case roundType.thinking:
                StopAllCoroutines();
                break;

        }
    }

    /** Start Anim to move 2 the positioning round */
    private IEnumerator StartAnim() {
        yield return new WaitForSeconds(0.5f);
        float timer = 0;

        float duration = 3f;
        while (timer < duration) {
            timer += Time.deltaTime;
            float percentageDuration = timer / duration;
            transform.position = Vector3.Lerp(transform.position, _positioningPosition.position, percentageDuration);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(_positioningPosition.localEulerAngles), percentageDuration);
            yield return new WaitForFixedUpdate();
        }

        transform.position = _positioningPosition.position;
        transform.rotation = _positioningPosition.rotation;
    }
    private IEnumerator AnimThinking()
    {
        int fovDir = -1;
        bool nano33canada = true;
        while (nano33canada)
        {
            Debug.Log("3QAAAAAAAAAAAAA");
            float breath = Mathf.Sin(Time.time * breathSpeed) * breathMagnitude;

            // Apply the breathing animation to the local position of the object
            Vector3 newPosition = transform.localPosition + new Vector3(0f, breath, 0f);
            _camera.fieldOfView += breath * breathFovSpeed * fovDir;
            transform.localPosition = newPosition;
            if(Mathf.Abs(_camera.fieldOfView - initialFov) > 1)
            {
                fovDir *= -1;
            }
            yield return null;
        }
    }
    private IEnumerator AnimAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _targetRot = new Vector3(25, 180, 0);
        float timer = 0;

        float duration = 3f;
        while (timer < duration)
        {
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CameraWall"))
        {
            _distance -= hasTouchedSpeed * Time.deltaTime;
            touchingWall = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        touchingWall = false;
    }

    private IEnumerator EndAnim() {
        yield return null;
        /*animating = true;
        Vector3 startEndPos = transform.position;
        Vector3 startEndRot = transform.eulerAngles;
        float timer = 0;

        while (timer < duration) {
            timer += Time.deltaTime;
            float percentageDuration = timer / duration;
            transform.position = Vector3.Lerp(startEndPos, finalPos, percentageDuration);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(startEndRot), Quaternion.Euler(finalRot), percentageDuration);
            yield return new WaitForFixedUpdate();
        }

        transform.position = finalPos;
        animating = false;
        transform.rotation = Quaternion.Euler(finalRot);*/
    }

}

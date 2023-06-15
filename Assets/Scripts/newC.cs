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

    [SerializeField, Header("Smooths:")]
    private float _rotSmoothTime;

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

    // Unity FixedUpdate
    void FixedUpdate() {
        if (!_active)
            return;

        cameraZoom();
        cameraRotation();

        // Clamps
        _distance = Mathf.Clamp(_distance, _zoomLimit.min, _zoomLimit.max);
        _targetRot.x = Mathf.Clamp(_targetRot.x, _verticalRotLimit.min, _verticalRotLimit.max);

        // Update de la posición
        transform.position = _pivot.position - transform.forward * _distance;
        //Update de la rotación
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
        if (Input.GetMouseButton(1)) {
            mouseX = Input.GetAxis("Mouse X") * _rootSpeed;
            mouseY = Input.GetAxis("Mouse Y") * _rootSpeed;
        }
        Vector3 nextRot = new Vector3(transform.localEulerAngles.x + mouseY, transform.localEulerAngles.y - mouseX);
        _targetRot = Vector3.SmoothDamp(transform.localEulerAngles, nextRot, ref _rotSmoothSpeed, _rotSmoothTime);
    }

    public void startRound(roundType round) {
        switch (round) {
            case roundType.positioning:
                StartCoroutine(StartAnim());
                break;
        }
    }

    public void endRound(roundType round) {
        switch (round) {
            case roundType.positioning:
                activate();
                break;
            case roundType.combat:
                StartCoroutine(EndAnim());
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

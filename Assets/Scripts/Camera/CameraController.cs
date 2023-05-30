using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField, Header("Canera Animator:")] 
    Animator _animator;

    #region EndPosition (C# Animation)
    [SerializeField] 
    Vector3 finalPos;
    [SerializeField] 
    Vector3 finalRot;
    #endregion

    #region StartMovement (C# Animation) 
    [SerializeField]
    private float duration;
    [SerializeField] 
    private Vector3 startAnimPos;
    [SerializeField]
    private Vector3 startAnimRot;
    [SerializeField]
    private Vector3 startAnimFinalPos;
    [SerializeField]
    private Vector3 startAnimFinalRot;
    #endregion



    public bool _active = false;
    public void Activate() {
        _active = true;
    }

    [SerializeField] float _speed;

    // Target que vamos a "seguir"
    [SerializeField, Header("Target:")]
    public Transform _target;

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

    [SerializeField] Transform cameraPos;
    bool animating = false;
    Vector3 targetPos;

    [SerializeField] float cameraMoveMovementSpeed;
    [SerializeField] float cameraMoveChangeTargetSpeed;
    float cameraSpeed;


    [SerializeField] float mouseSens = 3f;

    float rotX;
    float rotY;

    [SerializeField] Transform targetRotate;
    float distanceFromTarget;

    Vector3 currentRot;
    Vector3 smoothVel = Vector3.zero;

    [SerializeField] float smoothTime;

    bool firstTimeRotate = true;

    [SerializeField] Camera cam;

    [SerializeField] float maxFov;
    [SerializeField] float minFov;

    [SerializeField] float zoomMultiplier;
    float zoom;
    [SerializeField] float velocityZoom;
    [SerializeField] float smoothZoom;

    [SerializeField] float cameraMoveMultiplier;
    [SerializeField] float cameraMoveSum;

    float xAnterior, yAnterior;

    bool changeTarget = false;

    // Unity Draw Gizmos
    void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startAnimPos, 2f);
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(startAnimFinalPos, 2f);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(finalPos, 2f);
    }

    // Unity Awake
    void Awake() {
        zoom = cam.fieldOfView;
        rotX = transform.localEulerAngles.x;
        rotY = transform.localEulerAngles.y;
    }

    // Unity Start
    void Start() {
        StartCoroutine(StartAnim());
        xAnterior = 111111;
        xAnterior = 111111;
        TurnManager.instance.onStartTurn += () => { changeTarget = true; };
    }


    // Unity LateUpdate
    void LateUpdate() {

        if (!_active)
            return;

        Actor target;

        _target = TurnManager.instance.current.transform;
        target = _target.GetComponent<Actor>();

        if (changeTarget) {

            changeTarget = false;
            targetPos = new Vector3((target.getLastRouteNode().x - Stage.Grid.rows / 2.5f + cameraMoveSum) * cameraMoveMultiplier, 0, (target.getLastRouteNode().y - Stage.Grid.columns / 2.5f + cameraMoveSum) * cameraMoveMultiplier);
            cameraSpeed = cameraMoveChangeTargetSpeed;
        }

        if (!animating) {

            if (uCore.Action.GetKeyDown(KeyCode.Z)) {
                StartCoroutine(EndAnim());
            }
            if (!target.canMove()) {
                _animator.SetBool("zoom", true);
            }
            if (target.canMove()) {
                _animator.SetBool("zoom", false);
            }

            CameraTargetMove();
            CameraMouseMove();
            CameraZoom();

        }

    }

    private void CameraTargetMove() {

        Actor target = _target.GetComponent<Actor>();

        if (target.canMove()) {
            if (xAnterior != target.getLastRouteNode().x || yAnterior != target.getLastRouteNode().y) {
                xAnterior = target.getLastRouteNode().x;
                yAnterior = target.getLastRouteNode().y;
                targetPos = new Vector3((target.getLastRouteNode().x - Stage.Grid.rows / 2.5f + cameraMoveSum) * cameraMoveMultiplier, 0, (target.getLastRouteNode().y - Stage.Grid.columns / 2.5f + cameraMoveSum) * cameraMoveMultiplier);
                cameraSpeed = cameraMoveMovementSpeed;
            }

        } else {
            if (uCore.Action.GetKeyDown(KeyCode.U)) {
                targetPos = Vector3.zero;
                cameraSpeed = cameraMoveChangeTargetSpeed;
            }
        }
        Debug.Log(targetPos);
        cameraPos.localPosition = Vector3.Lerp(cameraPos.localPosition, targetPos, cameraSpeed * Time.deltaTime);
    }

    private void CameraMouseMove() {
        if (Input.GetMouseButton(1)) {
            distanceFromTarget = Vector3.Distance(targetRotate.position, transform.position);
            float mouseX = Input.GetAxis("Mouse X") * mouseSens;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

            rotX -= mouseY;
            rotY += mouseX;

            rotX = Mathf.Clamp(rotX, 20, 85);

            Vector3 nextRot = new Vector3(rotX, rotY);
            currentRot = Vector3.SmoothDamp(currentRot, nextRot, ref smoothVel, smoothTime);
            transform.localEulerAngles = currentRot;

            transform.position = targetRotate.position - transform.forward * distanceFromTarget;
        }
    }

    private void CameraZoom() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minFov, maxFov);
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, zoom, ref velocityZoom, smoothZoom);
    }

    IEnumerator StartAnim() {
        animating = true;
        transform.position = startAnimPos;
        transform.rotation = Quaternion.Euler(startAnimRot);
        yield return new WaitForSeconds(0.5f);
        float timer = 0;

        while (timer < duration) {
            timer += Time.deltaTime;
            float percentageDuration = timer / duration;
            transform.position = Vector3.Lerp(startAnimPos, startAnimFinalPos, percentageDuration);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(startAnimRot), Quaternion.Euler(startAnimFinalRot), percentageDuration);
            yield return new WaitForFixedUpdate();
        }

        transform.position = startAnimFinalPos;
        transform.rotation = Quaternion.Euler(startAnimFinalRot);
        animating = false;
    }

    IEnumerator EndAnim() {
        animating = true;
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
        transform.rotation = Quaternion.Euler(finalRot);
    }

    public void ChangeTarget() {

    }

}

using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] Animator _animator;

    //enter y exit animaciones
    [SerializeField] Vector3 finalPos;
    [SerializeField] Vector3 finalRot;
    float elapsedTime;
    [SerializeField] float duration;
    [SerializeField] Vector3 startAnimPos;
    [SerializeField] Vector3 startAnimRot;
    [SerializeField] Vector3 startAnimFinalPos;
    [SerializeField] Vector3 startAnimFinalRot;

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
    [SerializeField]
    public Grid2D grid {
        get {
            return Stage.StageGrid;
        }
        set {
        }
    }

    float xAnterior, yAnterior;

    bool changeTarget = false;
    private void Awake()
    {
        zoom = cam.fieldOfView;
        rotX = transform.localEulerAngles.x;
        rotY = transform.localEulerAngles.y;
    }

    private void Start() {
        StartCoroutine(StartAnim());
        xAnterior = 111111;
        xAnterior = 111111;
        TurnManager.instance.onStartTurn += () => { changeTarget = true; };

    }


    // Unity LateUpdate
    void LateUpdate() {

        if (changeTarget) {
            _target = TurnManager.instance.Current().transform;
            changeTarget = false;
            targetPos = new Vector3((_target.GetComponent<ActorMovement>().GetLastNode().x - grid.Rows / 2.5f + cameraMoveSum) * cameraMoveMultiplier, 0, (_target.GetComponent<ActorMovement>().GetLastNode().y - grid.Columns / 2.5f + cameraMoveSum) * cameraMoveMultiplier);
            cameraSpeed = cameraMoveChangeTargetSpeed;
        }

        if (!animating) {

            if (uCore.Action.GetKeyDown(KeyCode.Z)) {
                StartCoroutine(EndAnim());
            }
            if (!_target.GetComponent<Turnable>().canMove) {
                _animator.SetBool("zoom", true);
            }
            if (_target.GetComponent<Turnable>().canMove) {
                _animator.SetBool("zoom", false);
            }

            CameraTargetMove();
            CameraMouseMove();
            CameraZoom();

        }

    }
    private void CameraTargetMove()
    {
        if (_target.gameObject.GetComponent<ActorMovement>()._canMove)
        {
            if (xAnterior != _target.GetComponent<ActorMovement>().GetLastNode().x || yAnterior != _target.GetComponent<ActorMovement>().GetLastNode().y)
            {
                xAnterior = _target.GetComponent<ActorMovement>().GetLastNode().x;
                yAnterior = _target.GetComponent<ActorMovement>().GetLastNode().y;
                targetPos = new Vector3((_target.GetComponent<ActorMovement>().GetLastNode().x - grid.Rows / 2.5f  + cameraMoveSum) * cameraMoveMultiplier, 0, (_target.GetComponent<ActorMovement>().GetLastNode().y - grid.Columns / 2.5f + cameraMoveSum) * cameraMoveMultiplier);
                cameraSpeed = cameraMoveMovementSpeed;
            }

        }
        else
        {
            if (uCore.Action.GetKeyDown(KeyCode.U))
            {
                targetPos = Vector3.zero;
                cameraSpeed = cameraMoveChangeTargetSpeed;
            }
        }
        Debug.Log(targetPos);
        cameraPos.localPosition = Vector3.Lerp(cameraPos.localPosition, targetPos, cameraSpeed * Time.deltaTime);
    }
    private void CameraMouseMove()
    {
        if (Input.GetMouseButton(1))
        {
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

    private void CameraZoom()
    {
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

    //IEnumerator Prova()
    //{
    //    while (prova)
    //    {

    //    }
    //}

}

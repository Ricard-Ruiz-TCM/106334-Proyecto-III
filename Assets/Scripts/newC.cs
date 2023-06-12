using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newC : MonoBehaviour
{
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

    [SerializeField] float mouseSens = 3f;

    float rotX;
    float rotY;

    Vector3 currentRot;
    Vector3 smoothVel = Vector3.zero;
    [SerializeField] float smoothTime;

    [SerializeField] float zoomMultiplier;
    float zoom = 60;
    [SerializeField] float velocityZoom;
    [SerializeField] float smoothZoom;
    [SerializeField] float maxFov;
    [SerializeField] float minFov;
    [SerializeField] Camera cam;


    [SerializeField] Transform targetRotate;
    float distanceFromTarget;

    [SerializeField] Transform cameraPos;
    bool animating = false;
    [SerializeField]float cameraSpeed;

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

    [SerializeField]
    private Vector3 playAnimFinalPos;
    [SerializeField]
    private Vector3 playAnimFinalRot;

    bool canPlay;


    // Unity OnEnable
    void OnEnable()
    {
        BasicActor.onReAct += restoreZoom;
        BasicActor.onEndAct += restoreZoom;
        BasicActor.onStartAct += zoomOut;

        TurnManager.instance.onNewRound += startRound;
        TurnManager.instance.onEndRound += endRound;

        //Actor.onSkillUsed += focusNode;

        TurnManager.instance.onStartSystem += activate;
        TurnManager.instance.onStartTurn += () => { setTarget(TurnManager.instance.current); };
    }

    // Unity OnDisabel
    void OnDisable()
    {
        BasicActor.onReAct -= restoreZoom;
        BasicActor.onEndAct -= restoreZoom;
        BasicActor.onStartAct -= zoomOut;

        //Actor.onSkillUsed -= focusNode;

        //TurnManager.instance.onNewRound -= startRound;

        TurnManager.instance.onStartSystem -= activate;
        TurnManager.instance.onStartTurn -= () => { setTarget(TurnManager.instance.current); };
    }

    // Unity FixedUpdate
    void FixedUpdate()
    {
        if (!_active)
            return;

        if (_target != null)
        {
            // Behaviours
            if (!animating)
            {
                cameraRotation();
                //cameraFollow();
                cameraZoom();
            }

            //cameraLookAt();
            // Clamp
            //cameraClamp();
        }
        // Zoom
        

        // Suavizado del movimiento de la cámara
        //transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, _targetRot, _rotSpeed * Time.deltaTime);
    }

    /** Método para setear el target */
    public void setTarget(Turnable target)
    {
        StartCoroutine(play(target));
    }
    IEnumerator play(Turnable target)
    {
        if (!canPlay)
        {
            animating = true;
            yield return new WaitForSeconds(0.5f);
            float timer = 0;
            Vector3 startpos = transform.position;
            Quaternion startRot = transform.rotation;

            while (timer < duration)
            {
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

        }
        else
        {
            _targetPos.y = 8f;
            _target = target.transform;
        }
    }

    /** Método para activar la cámara */
    public void activate()
    {
        _active = true;
    }

    /** Método que levanta la camara like "zoom"  DOOOONE*/ 
    private void cameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minFov, maxFov);
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, zoom, ref velocityZoom, smoothZoom);
    }

    /** Método para rotar la camara desde el eje central  DOOOONE*/
    private void cameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            distanceFromTarget = Vector3.Distance(targetRotate.position, transform.position);
            float mouseX = Input.GetAxis("Mouse X") * mouseSens;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

            rotX -= mouseY;
            rotY += mouseX;

            rotX = Mathf.Clamp(rotX, 20, 75);

            Vector3 nextRot = new Vector3(rotX, rotY);
            currentRot = Vector3.SmoothDamp(currentRot, nextRot, ref smoothVel, smoothTime);
            transform.localEulerAngles = currentRot;

            transform.position = targetRotate.position - transform.forward * distanceFromTarget;
        }
    }

    /** Método para hacer lookAt de la camara */
    //private void cameraLookAt()
    //{
    //    _targetLookAtPosition = _target.position + Vector3.up * _cameraYTargetOffset;
    //    _targetRot = Quaternion.LookRotation(_targetLookAtPosition - transform.position);
    //}

    /** Método para la camara, que siempre siga al target */
    private void cameraFollow()
    {
        //if (_actorTarget.canMove())
        //{
        //    if (xAnterior != _actorTarget.getLastRouteNode().x || yAnterior != _actorTarget.getLastRouteNode().y)
        //    {
        //        xAnterior = _actorTarget.getLastRouteNode().x;
        //        yAnterior = _actorTarget.getLastRouteNode().y;
        //        // = new Vector3((_actorTarget.getLastRouteNode().x - Stage.Grid.rows / 2.5f + cameraMoveSum) * cameraMoveMultiplier, 0, (_actorTarget.getLastRouteNode().y - Stage.Grid.columns / 2.5f + cameraMoveSum) * cameraMoveMultiplier);
        //        cameraSpeed = cameraMoveMovementSpeed;
        //    }

        //}
        //else
        //{
        //    if (uCore.Action.GetKeyDown(KeyCode.U))
        //    {
        //        targetPos = Vector3.zero;
        //        cameraSpeed = cameraMoveChangeTargetSpeed;
        //    }
        //}

        cameraPos.localPosition = Vector3.Lerp(cameraPos.localPosition, _target.transform.position, cameraSpeed * Time.deltaTime);
        
    }

    /** Clamps para posicion y rotaicón */
    //private void cameraClamp()
    //{
    //    _targetPos.y = Mathf.Clamp(_targetPos.y, _zoomLimit.min, _zoomLimit.max);
    //}

    /** Métodos para hacer zoom a la haora de realizar un ataque */
    private void zoomOut()
    {
        _animator.SetBool("zoom", true);
    }
    private void restoreZoom()
    {
        _animator.SetBool("zoom", false);
    }

    /** Método para establecer el focus temporal */
    private void focusNode(Node target)
    {
        //if (target == null)
        //    return;
        //_temporalTarget = _target;
        //_target = Stage.StageBuilder.getGridPlane(target).transform;
        //StartCoroutine(CRestoreTarget(_focusTime));
    }

    ///** Méotod ede coroutine para volver al foco normal */
    //private IEnumerator CRestoreTarget(float delay)
    //{
    //    //yield return new WaitForSeconds(delay);
    //    //_target = _temporalTarget;
    //}

    public void startRound(roundType round)
    {
        switch (round)
        {
            case roundType.positioning:
                //_targetPos = _positioningPos.position;
                //_targetRot = _positioningPos.rotation;
                StartCoroutine(StartAnim());

                break;
        }
    }
    public void endRound(roundType round)
    {
        Debug.Log("TODO MAYBE ANIM");
    }
    IEnumerator StartAnim()
    {
        animating = true;
        transform.position = startAnimPos;
        transform.rotation = Quaternion.Euler(startAnimRot);
        yield return new WaitForSeconds(0.5f);
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float percentageDuration = timer / duration;
            transform.position = Vector3.Lerp(startAnimPos, startAnimFinalPos, percentageDuration);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(startAnimRot), Quaternion.Euler(startAnimFinalRot), percentageDuration);
            yield return new WaitForFixedUpdate();
        }

        transform.position = startAnimFinalPos;
        transform.rotation = Quaternion.Euler(startAnimFinalRot);
        animating = false;
        canPlay = false;
    }

    IEnumerator EndAnim()
    {
        animating = true;
        Vector3 startEndPos = transform.position;
        Vector3 startEndRot = transform.eulerAngles;
        float timer = 0;

        while (timer < duration)
        {
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
}

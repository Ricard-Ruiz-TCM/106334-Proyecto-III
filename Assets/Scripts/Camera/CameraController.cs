using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
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
    void LateUpdate() 
    {
        if (uCore.Action.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(EndAnim());
        }
        if (uCore.Action.GetKeyDown(KeyCode.C))
        {
            _animator.SetBool("zoom", true);
        }
        if (uCore.Action.GetKeyDown(KeyCode.X))
        {
            _animator.SetBool("zoom", false);
        }
    }
    private void Start()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
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
    }

    IEnumerator EndAnim()
    {
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
        transform.rotation = Quaternion.Euler(finalRot);
    }

    //IEnumerator Prova()
    //{
    //    while (prova)
    //    {

    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prova : MonoBehaviour
{
    [SerializeField] float mouseSens = 3f;

    float rotX;
    float rotY;

    [SerializeField] Transform target;
    [SerializeField] float distanceFromTarget;

    Vector3 currentRot;
    Vector3 smoothVel = Vector3.zero;

    [SerializeField] float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSens;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

            rotX -= mouseY;
            rotY += mouseX;

            rotX = Mathf.Clamp(rotX, -10, 85);

            Vector3 nextRot = new Vector3(rotX, rotY);
            currentRot = Vector3.SmoothDamp(currentRot, nextRot, ref smoothVel, smoothTime);
            transform.localEulerAngles = currentRot;

            transform.position = target.position - transform.forward * distanceFromTarget;
        }
        


        //if (Input.GetMouseButtonDown(0))
        //{
        //    previousPos = cam.ScreenToViewportPoint(Input.mousePosition);
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    cam.transform.position = target.position;

        //    Vector3 dir = previousPos - cam.ScreenToViewportPoint(Input.mousePosition);

        //    cam.transform.Rotate(Vector3.right, dir.y * 180);
        //    cam.transform.Rotate(Vector3.forward, -dir.x * 180, Space.World);
        //    cam.transform.Translate(new Vector3(0, 0, -10));

        //    previousPos = cam.ScreenToViewportPoint(Input.mousePosition);
        //}

    }
}

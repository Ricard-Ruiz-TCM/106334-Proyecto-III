using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraSpeed = 5f;
    public float zoomSpeed = 5f;
    public float rotationSpeed = 5f;

    public float minZoom = 1f;
    public float maxZoom = 10f;
    public float minPosX = -10f;
    public float maxPosX = 10f;
    public float maxPosY = 25f;
    public float minPosY = 5f;
    public float minPosZ = -10f;
    public float maxPosZ = 10f;
    public float minFOV = 10f;
    public float maxFOV = 60f;

    private float rotationY = 0f;

    public Transform target;

    private Vector3 targetPosition;

    private void Start() {
        targetPosition = transform.position;
    }

    // Unity FixedUpdate
    void FixedUpdate() {
        if (target == null) {
            CameraFreeMovement();
        } else {
            CameraFollowMovement();
        }   

        CameraZoom();
        CameraRotation();

        // Suavizado del movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }

    private void CameraZoom() {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        float desiredZoom = scrollAmount * zoomSpeed;
        desiredZoom = Mathf.Clamp(desiredZoom, -maxZoom, maxZoom);
        targetPosition += transform.forward * desiredZoom;
        ClampCameraPosition();
    }

    private void CameraRotation() {
        if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftAlt)) {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            rotationY += mouseX;
            Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotationY, transform.rotation.eulerAngles.z);
            transform.rotation = rotation;
        }
    }

    private void CameraFollowMovement() {
        Vector3 targetFollowPosition = target.position;
        targetFollowPosition.y = transform.position.y;
        targetPosition = targetFollowPosition;
        ClampCameraPosition();
    }

    private void CameraFreeMovement() {
        if (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftAlt)) {
            float mouseX = -Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");
            Vector3 desiredMovement = GetCameraMovement(mouseX, mouseY);
            targetPosition += desiredMovement;
            ClampCameraPosition();
        }
    }

    private Vector3 GetCameraMovement(float mouseX, float mouseY) {
        Quaternion rotation = Quaternion.Euler(0f, rotationY, 0f);
        Vector3 cameraMovement = new Vector3(mouseX, 0f, mouseY) * cameraSpeed * Time.deltaTime;
        cameraMovement = rotation * cameraMovement;
        return cameraMovement;
    }

    private void ClampCameraPosition() {
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosX, maxPosX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosY, maxPosY);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minPosZ, maxPosZ);
    }

}


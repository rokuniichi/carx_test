using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject cameraBase;
    [SerializeField] private GameObject cameraRig;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomMin;
    [SerializeField] private float zoomMax;

    private void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleZoom();
    }

    private void HandleRotation()
    {
        if (!KeyboardInput.CameraHold) return;
        float rotation = KeyboardInput.CameraRotation;
        cameraBase.transform.rotation = Quaternion.Euler(0f, rotation * rotationSpeed + cameraBase.transform.eulerAngles.y, 0f);
    }

    private void HandleMovement()
    {
        Vector3 direction = (KeyboardInput.Horizontal * cameraBase.transform.right + KeyboardInput.Vertical * cameraBase.transform.forward).normalized;
        cameraBase.transform.position += moveSpeed * Time.deltaTime * direction;
    }

    private void HandleZoom()
    {
        Vector3 direction = (KeyboardInput.CameraZoom * cameraRig.transform.forward).normalized;
        Vector3 targetPosition = cameraRig.transform.position + zoomSpeed * Time.deltaTime * direction;
        float distance = Vector3.Distance(cameraBase.transform.position, targetPosition);
        if (distance < zoomMin || distance > zoomMax) return;
        cameraRig.transform.position = targetPosition;
    }
}

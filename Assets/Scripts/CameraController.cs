using UnityEngine;

[RequireComponent(typeof(IInputGetter))]
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

    private IInputGetter inputGetter;
    private void Awake()
    {
        inputGetter = GetComponent<IInputGetter>();
    }

    private void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleZoom();
    }

    private void HandleRotation()
    {
        if (!inputGetter.CameraHold) return;
        float rotation = inputGetter.CameraRotation;
        cameraBase.transform.rotation = Quaternion.Euler(0f, rotation * rotationSpeed + cameraBase.transform.eulerAngles.y, 0f);
    }

    private void HandleMovement()
    {
        Vector3 direction = (inputGetter.Horizontal * cameraBase.transform.right + inputGetter.Vertical * cameraBase.transform.forward).normalized;
        cameraBase.transform.position += moveSpeed * Time.deltaTime * direction;
    }

    private void HandleZoom()
    {
        Vector3 direction = (inputGetter.CameraZoom * cameraRig.transform.forward).normalized;
        Vector3 targetPosition = cameraRig.transform.position + zoomSpeed * Time.deltaTime * direction;
        float distance = Vector3.Distance(cameraBase.transform.position, targetPosition);
        if (distance < zoomMin || distance > zoomMax) return;
        cameraRig.transform.position = targetPosition;
    }
}

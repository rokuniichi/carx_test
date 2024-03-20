using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputGetter
{
    public float Horizontal { get; private set; }

    public float Vertical { get; private set; }

    public float CameraRotation { get; private set; }

    public float CameraZoom { get; private set; }

    public bool CameraHold { get; private set; }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        CameraRotation = Input.GetAxisRaw("Mouse X");
        CameraZoom = Input.GetAxisRaw("Mouse ScrollWheel");
        CameraHold = Input.GetMouseButton(1);
    }
}

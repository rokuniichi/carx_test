using UnityEngine;

public static class KeyboardInput
{
    public static float Horizontal => Input.GetAxisRaw("Horizontal");
    public static float Vertical => Input.GetAxisRaw("Vertical");
    public static float CameraRotation => Input.GetAxisRaw("Mouse X");
    public static float CameraZoom => Input.GetAxisRaw("Mouse ScrollWheel");
    public static bool CameraHold => Input.GetMouseButton(1);
}

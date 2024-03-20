internal interface IInputGetter
{
    float Horizontal { get; }
    float Vertical { get; }
    float CameraRotation { get; }
    float CameraZoom { get; }
    bool CameraHold { get;  }
}
using UnityEngine;

internal interface IPredictable
{ 
    Vector3 Velocity { get; }
    Vector3 GetPositionInTime(float time);
}
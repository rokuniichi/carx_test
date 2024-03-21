using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] 
public class AimRotationEvent : UnityEvent<Transform, float> { }

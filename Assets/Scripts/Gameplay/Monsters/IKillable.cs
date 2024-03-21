using System;
using UnityEngine;

public interface IKillable
{
    Action<Transform> OnKill { get; set; }
    void KillSelf();
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AimRotation : MonoBehaviour, ITowerSystem
{
    [Serializable]
    public class RotationTarget
    {
        [SerializeField] public Transform transform;
        [SerializeField] public float rotationSpeed;
        [SerializeField] public bool lockX;
        [SerializeField] public bool lockY;
        [SerializeField] public bool lockZ;
    }

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float accuracyMargin;
    [SerializeField] List<RotationTarget> rotationTargets;
    [SerializeField] private OnCannonAim onAim;

    private Transform _currentTarget;
    private float _projectileSpeed;

    public void Init(TowerData towerData)
    {
        _projectileSpeed = towerData.ProjectileData.Speed;
    }

    public void SetAim(Transform target)
    {
        _currentTarget = target;
    }

    // Solution by AntonVelmozhniy @ https://github.com/AntonVelmozhniy
    private float GetTimeToHit(Vector3 targetPosition, Vector3 targetVelocity, Vector3 attackerPosition, float projectileSpeed)
    {
        Vector3 direction = targetPosition - attackerPosition;
        direction.y = 0;
        targetVelocity.y = 0;
        float a = Vector3.Dot(targetVelocity, targetVelocity) - (projectileSpeed * projectileSpeed);
        float b = 2 * Vector3.Dot(targetVelocity, direction); 
        float c = Vector3.Dot(direction, direction); 
        float D = Mathf.Sqrt((b * b) - 4 * a * c);
        float t1 = (-b + D) / (2 * a);
        float t2 = (-b - D) / (2 * a);
        float time = Mathf.Max(t1, t2);
        return time;
    }

    private void LateUpdate()
    {
        if (_currentTarget == null) return;
        IPredictable targetPrediction = _currentTarget.GetComponent<IPredictable>();
        if (targetPrediction == null) return;
        float time = GetTimeToHit(_currentTarget.position, targetPrediction.Velocity, shootingPoint.position, _projectileSpeed);
        Vector3 hitPoint = targetPrediction.GetPositionInTime(time);
        Vector3 projectedDirection = hitPoint - shootingPoint.position;
        projectedDirection.y = 0;
        float antiGravity = -Physics.gravity.y * time / 2;
        float deltaY = (hitPoint.y - shootingPoint.position.y) / time;
        Vector3 projectedVelocity = projectedDirection.normalized * _projectileSpeed;
        projectedVelocity.y = antiGravity + deltaY;
        foreach (RotationTarget rotationTarget in rotationTargets)
        {
            Quaternion lookRotation = Quaternion.LookRotation(projectedVelocity);
            lookRotation.x = rotationTarget.lockX ? rotationTarget.transform.localRotation.x : lookRotation.x;
            lookRotation.y = rotationTarget.lockY ? rotationTarget.transform.localRotation.y : lookRotation.y;
            lookRotation.z = rotationTarget.lockZ ? rotationTarget.transform.localRotation.z : lookRotation.z;
            rotationTarget.transform.localRotation = Quaternion.Slerp(rotationTarget.transform.localRotation, lookRotation, Time.deltaTime * rotationTarget.rotationSpeed);
            //rotationTarget.transform.localRotation = Quaternion.RotateTowards(rotationTarget.transform.localRotation, lookRotation, Time.deltaTime * rotationTarget.rotationSpeed);
        }

        Vector3 currentVelocity = shootingPoint.forward * _projectileSpeed;
        Debug.DrawLine(shootingPoint.position, shootingPoint.position + projectedVelocity * time, Color.red);
        Debug.DrawLine(shootingPoint.position, shootingPoint.position + currentVelocity * time, Color.blue);
        float difference = Vector3.Distance(currentVelocity, projectedVelocity);
        if (Vector3.Angle(currentVelocity, projectedVelocity) < accuracyMargin)
            onAim?.Invoke(projectedVelocity);
    }
}

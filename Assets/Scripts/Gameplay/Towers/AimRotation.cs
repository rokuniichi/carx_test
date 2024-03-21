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
    private Vector3 GetHitPoint(Vector3 targetPosition, Vector3 targetSpeed, Vector3 attackerPosition, float projectileSpeed, out float time)
    {
        Vector3 direction = targetPosition - attackerPosition;
        direction.y = 0;
        targetSpeed.y = 0;
        float a = Vector3.Dot(targetSpeed, targetSpeed) - (projectileSpeed * projectileSpeed);
        float b = 2 * Vector3.Dot(targetSpeed, direction); 
        float c = Vector3.Dot(direction, direction); 
        float D = Mathf.Sqrt((b * b) - 4 * a * c);
        float t1 = (-b + D) / (2 * a);
        float t2 = (-b - D) / (2 * a);
        time = Mathf.Max(t1, t2);
        Vector3 result = targetPosition + targetSpeed * time;
        return result;
    }

    private void LateUpdate()
    {
        if (_currentTarget == null) return;
        float time = 0;
        Vector3 hitPoint = GetHitPoint(_currentTarget.position, _currentTarget.GetComponent<IPredictable>().Velocity, shootingPoint.position, _projectileSpeed, out time);
        Vector3 projectedDirection = hitPoint - transform.position;
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
            //rotationTarget.transform.localRotation = Quaternion.RotateTowards(rotationTarget.transform.localRotation, lookRotation, Time.deltaTime * rotationTarget.rotationSpeed);
            rotationTarget.transform.localRotation = lookRotation;
        }

        Vector3 currentDirection = shootingPoint.forward;
        currentDirection.y = 0;
        Vector3 currentVelocity = currentDirection.normalized * _projectileSpeed;
        currentVelocity.y = antiGravity + deltaY;

        float difference = Vector3.Distance(currentVelocity, projectedVelocity);
        Debug.DrawLine(shootingPoint.position, currentVelocity, Color.blue);
        Debug.DrawLine(shootingPoint.position, projectedVelocity, Color.red);
        if (difference < accuracyMargin)
            onAim?.Invoke(currentVelocity);
    }
}

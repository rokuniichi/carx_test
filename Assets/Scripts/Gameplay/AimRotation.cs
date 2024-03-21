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
    [SerializeField] private UnityEvent onAim;

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

    private void Update()
    {
        if (_currentTarget == null) return;
        float time = 0f;
        Vector3 targetPoint = GetHitPoint(_currentTarget.position, _currentTarget.GetComponent<IPredictable>().LastSpeed, shootingPoint.position, _projectileSpeed, out time);
        foreach (RotationTarget rotationTarget in rotationTargets)
        {
            Vector3 baseDirection = (targetPoint - rotationTarget.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(baseDirection);
            lookRotation.x = rotationTarget.lockX ? rotationTarget.transform.localRotation.x : lookRotation.x;
            lookRotation.y = rotationTarget.lockY ? rotationTarget.transform.localRotation.y : lookRotation.y;
            lookRotation.z = rotationTarget.lockZ ? rotationTarget.transform.localRotation.z : lookRotation.z;
            rotationTarget.transform.localRotation = Quaternion.RotateTowards(rotationTarget.transform.localRotation, lookRotation, Time.deltaTime * rotationTarget.rotationSpeed);
        }
       
        float distance = Vector3.Distance(shootingPoint.position + shootingPoint.forward * _projectileSpeed * time, targetPoint);
        if (distance < accuracyMargin)
            onAim?.Invoke();
    }
}

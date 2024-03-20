using System.Collections;
using UnityEngine;

public class CannonTower : BaseTower {
    [SerializeField] private float rotationSpeed;
    [SerializeField] protected Transform cannonBody;
    [SerializeField] protected Transform cannonBase;

    protected override void Fire()
    {
        ProjectileSpawner<CannonProjectile>.SpawnProjectile(_projectileData, shootingPoint);
        StartCoroutine(RotationRoutine());
    }

    // Solution by AntonVelmozhniy @ https://github.com/AntonVelmozhniy
    Vector3 GetHitPoint(Vector3 targetPosition, Vector3 targetSpeed, Vector3 attackerPosition, float bulletSpeed)
    {
        Vector3 direction = targetPosition - attackerPosition;
        float a = Vector3.Dot(targetSpeed, targetSpeed) - (bulletSpeed * bulletSpeed);
        float b = 2 * Vector3.Dot(targetSpeed, direction);
        float c = Vector3.Dot(direction, direction);
        float D = Mathf.Sqrt((b * b) - 4 * a * c);
        float t1 = (-b + D) / (2 * a);
        float t2 = (-b - D) / (2 * a);
        float time = Mathf.Max(t1, t2);
        Vector3 result = targetPosition + targetSpeed * time;
        return result;
    }

    IEnumerator RotationRoutine()
    {
        while (_currentTarget != null)
        {
            Vector3 targetPoint = GetHitPoint(_currentTarget.transform.position, _currentTarget.GetComponent<IPredictable>().LastSpeed, shootingPoint.transform.position, _projectileData.Speed);

            Vector3 baseDirection = (targetPoint - cannonBase.position).normalized;
            Quaternion baseLook = Quaternion.LookRotation(baseDirection);
            baseLook.x = cannonBase.localRotation.x;
            baseLook.z = cannonBase.localRotation.z;
            cannonBase.localRotation = Quaternion.RotateTowards(cannonBase.localRotation, baseLook, Time.deltaTime * rotationSpeed);

            Vector3 bodyDirection = (targetPoint - cannonBody.position).normalized;
            Quaternion bodyLook = Quaternion.LookRotation(bodyDirection);
            bodyLook.y = cannonBody.localRotation.y;
            bodyLook.z = cannonBody.localRotation.z;
            cannonBody.localRotation = Quaternion.RotateTowards(cannonBody.localRotation, bodyLook, Time.deltaTime * rotationSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}

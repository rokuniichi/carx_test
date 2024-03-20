using UnityEngine;

public class CannonTower : BaseTower {
    [SerializeField] protected GameObject cannon;
    protected override void Fire()
    {
        Vector3 targetPoint = GetHitPoint(_currentTarget.transform.position, _currentTarget.GetComponent<IPredictable>().LastSpeed, shootingPoint.transform.position, _projectileData.Speed);
        cannon.transform.LookAt(targetPoint);
        SpawnProjectile();
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
}

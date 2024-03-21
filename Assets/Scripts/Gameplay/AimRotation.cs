using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;
    [SerializeField] private Transform shootingPoint;

    private float _projectileSpeed;
    private Transform _currentTarget;

    public void SetAim(Transform target, float _speed)
    {
        _currentTarget = target;
        _projectileSpeed = _speed;
    }

    // Solution by AntonVelmozhniy @ https://github.com/AntonVelmozhniy
    private Vector3 GetHitPoint(Vector3 targetPosition, Vector3 targetSpeed, Vector3 attackerPosition, float projectileSpeed)
    {
        Vector3 direction = targetPosition - attackerPosition;
        float a = Vector3.Dot(targetSpeed, targetSpeed) - (projectileSpeed * projectileSpeed);
        float b = 2 * Vector3.Dot(targetSpeed, direction);
        float c = Vector3.Dot(direction, direction);
        float D = Mathf.Sqrt((b * b) - 4 * a * c);
        float t1 = (-b + D) / (2 * a);
        float t2 = (-b - D) / (2 * a);
        float time = Mathf.Max(t1, t2);
        Vector3 result = targetPosition + targetSpeed * time;
        return result;
    }

    private void Update()
    {
        if (_currentTarget == null) return;
        Vector3 targetPoint = GetHitPoint(_currentTarget.position, _currentTarget.GetComponent<IPredictable>().LastSpeed, shootingPoint.position, _projectileSpeed);
        Vector3 baseDirection = (targetPoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(baseDirection);
        lookRotation.x = lockX ? transform.localRotation.x : lookRotation.x;
        lookRotation.y = lockY ? transform.localRotation.y : lookRotation.y;
        lookRotation.z = lockZ ? transform.localRotation.z : lookRotation.z;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}

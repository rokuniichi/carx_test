using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected float _speed;
    protected float _damage;

    public void Init(ProjectileData projectileData)
    {
        _speed = projectileData.Speed;
        _damage = projectileData.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable obj = other.GetComponent<IDamagable>();
        if (obj == null) return;

        obj.ReceiveDamage(_damage);
        PoolManager.Instance.Remove(gameObject);
    }
}

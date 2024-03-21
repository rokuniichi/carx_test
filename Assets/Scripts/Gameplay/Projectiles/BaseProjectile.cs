using UnityEngine;
using UnityEngine.Events;

public class BaseProjectile : MonoBehaviour
{
    protected float speed;
    protected float damage;

    public void Init(ProjectileData projectileData)
    {
        speed = projectileData.Speed;
        damage = projectileData.Damage;
    }

    public void SelfRemove()
    {
        PoolManager.Instance.Remove(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable obj = other.GetComponent<IDamagable>();
        if (obj == null) return;

        obj.ReceiveDamage(damage);
        SelfRemove();
    }
}

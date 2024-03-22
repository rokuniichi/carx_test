using UnityEngine;
using UnityEngine.Events;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected UnityEvent onSelfRemove;

    protected float speed;
    protected float damage;

    public void Init(ProjectileData projectileData)
    {
        speed = projectileData.Speed;
        damage = projectileData.Damage;
    }

    public virtual void SelfRemove()
    {
        onSelfRemove?.Invoke();
        PoolManager.Instance.Remove(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable obj = other.GetComponent<IDamagable>();
        if (obj != null)
        {
            obj.ReceiveDamage(damage);
            SelfRemove();
        }
    }
}

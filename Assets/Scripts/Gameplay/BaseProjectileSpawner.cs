using UnityEngine;
using UnityEngine.Events;

public abstract class BaseProjectileSpawner : MonoBehaviour, IProjectileSpawner, ITowerSystem
{
    [SerializeField] protected Transform shootingPoint;
    [SerializeField] protected UnityEvent onProjectileSpawned;

    protected ProjectileData projectileData;

    public void Init(TowerData towerData)
    {
        projectileData = towerData.ProjectileData;
    }

    public abstract void SpawnProjectile();
}

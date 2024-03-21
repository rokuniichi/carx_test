using UnityEngine;
using UnityEngine.Events;

public abstract class BaseProjectileSpawner : MonoBehaviour, IProjectileSpawner
{
    [SerializeField] protected ProjectileData projectileData;
    [SerializeField] protected Transform spawnLocation;
    [SerializeField] protected UnityEvent onProjectileSpawned;

    public void Init(ProjectileData newProjectileData, Transform newSpawnLocation)
    {
        projectileData = newProjectileData;
        spawnLocation = newSpawnLocation;
    }

    public abstract void SpawnProjectile();
}

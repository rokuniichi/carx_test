using UnityEngine;

public class CannonProjectileSpawner : BaseProjectileSpawner
{
    public override void SpawnProjectile()
    {
        ProjectileFactory<CannonProjectile>.CreateProjectileOfType(projectileData, spawnLocation);
        onProjectileSpawned?.Invoke();
    }
}

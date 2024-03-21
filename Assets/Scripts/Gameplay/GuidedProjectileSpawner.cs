using UnityEngine;

public class GuidedProjectileSpawner : BaseProjectileSpawner
{
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public override void SpawnProjectile()
    {
        if (_target == null) return;
        GuidedProjectile proj = ProjectileFactory<GuidedProjectile>.CreateProjectileOfType(projectileData, spawnLocation);
        proj.SetTarget(_target);
        onProjectileSpawned?.Invoke();
    }
}

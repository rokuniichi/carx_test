using UnityEngine;

public class CannonProjectileSpawner : BaseProjectileSpawner
{
    private Vector3 _velocity;

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public override void SpawnProjectile()
    {
        var proj = ProjectileFactory<BaseProjectile>.CreateProjectileOfType(projectileData, shootingPoint);
        proj.GetComponent<Rigidbody>().velocity = _velocity;
        onProjectileSpawned?.Invoke();
    }
}

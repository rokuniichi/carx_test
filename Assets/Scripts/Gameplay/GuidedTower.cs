public class GuidedTower : BaseTower
{
    protected override void Fire()
    {
		GuidedProjectile projectile = ProjectileSpawner<GuidedProjectile>.SpawnProjectile(_projectileData, shootingPoint);
		projectile.SetTarget(_currentTarget);
	}
}

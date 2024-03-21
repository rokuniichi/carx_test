public class GuidedTower : BaseTower
{
    protected override void OnFire()
    {
		GuidedProjectile projectile = ProjectileSpawner<GuidedProjectile>.SpawnProjectile(ProjectileData, shootingPoint);
		projectile.SetTarget(_currentTarget);
	}
}

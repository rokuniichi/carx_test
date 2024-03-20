public class GuidedTower : BaseTower
{
    protected override void Fire()
    {
		GuidedProjectile projectile = SpawnProjectile() as GuidedProjectile;
		projectile.SetTarget(_currentTarget);
	}
}

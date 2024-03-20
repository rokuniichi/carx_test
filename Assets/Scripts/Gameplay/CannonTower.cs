using UnityEngine;

public class CannonTower : BaseTower {
    protected override void Fire()
    {
        SpawnProjectile();
    }
}

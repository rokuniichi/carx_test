using System;
using System.Collections;
using UnityEngine;

public class CannonTower : BaseTower {
    protected override void OnFire()
    {
        ProjectileSpawner<CannonProjectile>.SpawnProjectile(ProjectileData, shootingPoint);
    }
}

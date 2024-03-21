using System;
using System.Collections;
using UnityEngine;

public class CannonTower : BaseTower {
    [SerializeField] private AimRotationEvent aimRotationEvent;
    protected override void Fire()
    {
        ProjectileSpawner<CannonProjectile>.SpawnProjectile(_projectileData, shootingPoint);
    }

    protected override void OnSetTarget()
    {
        aimRotationEvent?.Invoke(_currentTarget, _projectileData.Speed);
    }
}

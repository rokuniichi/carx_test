using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileSpawner
{
    void Init(ProjectileData projectileData, Transform spawnLocation);
    void SpawnProjectile();
}

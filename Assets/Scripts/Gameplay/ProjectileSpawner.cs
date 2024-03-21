using UnityEngine;

public static class ProjectileSpawner<T> where T : BaseProjectile
{
    public static T SpawnProjectile(ProjectileData projectileData, Transform source)
    {
        GameObject obj = PoolManager.Instance.Create(projectileData.Prefab);
        obj.transform.position = source.position;
        obj.transform.rotation = source.rotation;
        T proj = obj.GetComponent<T>();
        proj.Init(projectileData);
        return proj;
    }
}
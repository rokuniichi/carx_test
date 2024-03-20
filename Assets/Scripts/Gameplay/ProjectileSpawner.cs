using UnityEngine;

public static class ProjectileSpawner<T> where T : BaseProjectile
{
    public static T SpawnProjectile(ProjectileData projectileData, Transform parent)
    {
        GameObject obj = PoolManager.Instance.Create(projectileData.Prefab);
        obj.transform.SetParent(parent);
        obj.transform.position = parent.position;
        obj.transform.rotation = parent.rotation;
        T proj = obj.GetComponent<T>();
        proj.Init(projectileData);
        return proj;
    }
}
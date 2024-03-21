using UnityEngine;

public static class ProjectileFactory<T> where T : BaseProjectile
{
    public static T CreateProjectileOfType(ProjectileData projectileData, Transform parent)
    {
        GameObject obj = PoolManager.Instance.Create(projectileData.Prefab);
        obj.transform.position = parent.position;
        obj.transform.rotation = parent.rotation;
        T proj = obj.GetComponent<T>();
        proj.Init(projectileData);
        return proj;
    }
}
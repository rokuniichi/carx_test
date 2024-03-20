using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance => _instance;

    private static PoolManager _instance;

    private Dictionary<string, Queue<GameObject>> _pools;

    private void Awake()
    {
        _instance = this;
        _pools = new Dictionary<string, Queue<GameObject>>();
    }

    public GameObject Create(GameObject prefab)
    {
        GameObject result = GetObjectFromPool(prefab.tag);
        if (result == null)
            result = Instantiate(prefab);

        result.SetActive(true);
        return result;
    }

    public void Remove(GameObject obj)
    {
        obj.SetActive(false);
        AddObjectIntoPool(obj);
    }

    private void AddObjectIntoPool(GameObject obj)
    {
        if (!_pools.ContainsKey(obj.tag))
        {
            _pools[obj.tag] = new Queue<GameObject>();
        }

        _pools[obj.tag].Enqueue(obj);
    }

    private GameObject GetObjectFromPool(string objTag)
    {
        if (!_pools.ContainsKey(objTag) || _pools[objTag].Count == 0)
            return null;

        return _pools[objTag].Dequeue();
    }
}

using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    private static Dictionary<string, Queue<GameObject>> GameObjects;

    public static void Init()
    {
        GameObjects = new Dictionary<string, Queue<GameObject>>();
    }

    public static void AddObjectIntoPool(string tag, GameObject obj)
    {
        if (GameObjects[tag] == null)
            GameObjects[tag] = new Queue<GameObject>();

        GameObjects[tag].Enqueue(obj);
    }

    public static GameObject GetObjectFromPool(string tag)
    {
        if (GameObjects[tag] == null || GameObjects[tag].Count == 0)
            return null;

        return GameObjects[tag].Dequeue();
    }
}

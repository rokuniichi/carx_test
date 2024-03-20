using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    public static LevelData LoadLevel(int number)
    {
        LevelData ld = Resources.Load<LevelData>($"Data/LevelData/L{number}");
        if (ld == null) {
            Debug.LogError($"Level {number} not found!");
            return null;
        }

        return ld;
    } 
}

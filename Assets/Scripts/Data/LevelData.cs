using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelData : ScriptableObject
{
    [Header("General")]
    [SerializeField] public int LevelNumber;
    [Header("References")]
    [SerializeField] public GameObject BasicCell;
    [SerializeField] public GameObject PathCell;
    [Header("Grid")]
    [SerializeField] public int GridWidth;
    [SerializeField] public int GridHeight;
    [SerializeField] public float GridSpacing;
    [SerializeField] public List<Vector2Int> GridPath;
    [Header("Difficulty")]
    [SerializeField] public int MonsterAmount;
}

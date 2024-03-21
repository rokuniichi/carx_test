using UnityEngine;

[CreateAssetMenu()]
public class TowerData : ScriptableObject
{
    [Header("References")]
    [SerializeField] public GameObject BasePrefab;
    [SerializeField] public GameObject WeaponPrefab;
    [SerializeField] public ProjectileData ProjectileData;
    [Header("Visuals")]
    [SerializeField] public Vector3 WeaponPlacementOffset;
    [Header("Stats")]
    [SerializeField] public float Cooldown;
    [SerializeField] public float Range;
    [SerializeField] public LayerMask TargetLayers;
}

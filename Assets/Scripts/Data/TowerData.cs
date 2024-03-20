using UnityEngine;

[CreateAssetMenu()]
public class TowerData : ScriptableObject
{
    [Header("References")]
    [SerializeField] public GameObject BasePrefab;
    [SerializeField] public GameObject WeaponPrefab;
    [Header("Visuals")]
    [SerializeField] public Vector3 WeaponPlacementOffset;
    [Header("Stats")]
    [SerializeField] public float Damage;
    [SerializeField] public float Cooldown;
    [SerializeField] public float Range;
    [SerializeField] public float ProjectileRadius;
    [SerializeField] public float ProjectileSpeed;
}

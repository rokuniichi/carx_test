using UnityEngine;

[CreateAssetMenu()]
public class ProjectileData : ScriptableObject
{
    [Header("References")]
    [SerializeField] public GameObject Prefab;
    [Header("Stats")]
    [SerializeField] public float Damage;
    [SerializeField] public float Speed;
}

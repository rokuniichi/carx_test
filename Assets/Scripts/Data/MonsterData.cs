using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MonsterData : ScriptableObject
{
    [Header("References")]
    [SerializeField] public GameObject Prefab;
    [Header("Stats")]
    [SerializeField] public float MaxHealth;
    [SerializeField] public float Speed;
}

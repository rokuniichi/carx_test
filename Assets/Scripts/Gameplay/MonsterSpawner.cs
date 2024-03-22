using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour {
	[SerializeField] private List<Transform> path;

	// Temporary until proper data flow
	[SerializeField] private MonsterData monsterTemplate;	
	public void SpawnMonster()
    {
		Monster monster = PoolManager.Instance.Create(monsterTemplate.Prefab).GetComponent<Monster>();
		monster.Init(monsterTemplate, path);
		monster.transform.SetParent(gameObject.transform);
		monster.transform.position = path[0].transform.position;
    }
}

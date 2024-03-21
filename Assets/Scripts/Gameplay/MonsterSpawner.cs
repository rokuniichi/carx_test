using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour {
	[SerializeField] private float interval;
	[SerializeField] private List<Transform> path;

	// Temporary until proper data flow
	[SerializeField] private MonsterData monsterTemplate;

	private Timer _timer;

    private void Start()
    {
		_timer = new Timer();
    }

    void Update () {
		_timer.Tick(Time.deltaTime);
		if (!(_timer.CurrentDuration > 0))
		{
			SpawnMonster();
			_timer.Start(interval);
		}
	}
	
	private void SpawnMonster()
    {
		Monster monster = PoolManager.Instance.Create(monsterTemplate.Prefab).GetComponent<Monster>();
		monster.Init(monsterTemplate, path);
		monster.transform.SetParent(gameObject.transform);
		monster.transform.position = path[0].transform.position;
    }
}

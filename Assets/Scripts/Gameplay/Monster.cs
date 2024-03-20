using UnityEngine;
using System;
using System.Collections.Generic;

public class Monster : MonoBehaviour, IKillable, IDamagable, IPoolable {

	private MonsterData _monsterData;
	private List<Transform> _path;

	private float _health;
	private float _speed;

	private Transform _moveTarget;

	private int _currentIndex;

	public Action<Transform> OnKill { get; set; }

	public void Init(MonsterData monsterData, List<Transform> path)
    {
		_monsterData = monsterData;
		_path = path;
		Reset();
    }

	public void Reset()
	{
		_currentIndex = 0;
		_health = _monsterData.MaxHealth;
		_speed = _monsterData.Speed;
		_moveTarget = _path[_currentIndex];
	}

	public void KillSelf()
	{
		OnKill?.Invoke(transform);
		PoolManager.Instance.Remove(gameObject);
	}

	public void ReceiveDamage(float damage)
    {
		_health -= damage;
		if (_health <= 0f)
		{
			KillSelf();
		}
	}
	
	private void Update()
	{
		if (_moveTarget == null)
			return;

		Vector3 translation = _moveTarget.transform.position - transform.position;
		float frameDistance = _speed * Time.deltaTime;
		if (translation.magnitude > frameDistance)
		{
			transform.Translate(translation.normalized * frameDistance);
		}
		else
		{
			if (_currentIndex == _path.Count)
			{
				PoolManager.Instance.Remove(gameObject);
				return;
			}
			else
			{
				_moveTarget = _path[_currentIndex++];
			}
		}

	}
}

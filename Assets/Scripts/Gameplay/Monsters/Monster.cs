using UnityEngine;
using System;
using System.Collections.Generic;

public class Monster : MonoBehaviour, IKillable, IDamagable, IPredictable {

	public Action<Transform> OnKill { get; set; }
	public Vector3 Velocity => _velocity;

	private MonsterData _monsterData;
	private List<Transform> _path;

	private float _health;
	private float _speed;

	private Vector3 _velocity;

	private Transform _moveTarget;

	private int _currentIndex;

    public void Init(MonsterData monsterData, List<Transform> path)
    {
		_monsterData = monsterData;
		_path = path;
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

		Vector3 direction = _moveTarget.transform.position - transform.position;
		float frameDistance = _speed * Time.deltaTime;
		_velocity = direction.normalized * _speed;
		if (direction.magnitude > frameDistance)
		{
			transform.position += _velocity * Time.deltaTime;
			transform.forward = _velocity;
		}
		else
		{
			if (_currentIndex == _path.Count)
			{
				KillSelf();
				return;
			}
			else
            {
				_moveTarget = _path[_currentIndex++];
			}
				
		}
	}
}

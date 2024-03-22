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
	private Vector3 _direction => _moveTarget.transform.position - transform.position;

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

	// TODO: Translate to path
	public Vector3 GetPositionInTime(float time)
	{
		Debug.DrawLine(transform.position, transform.position + _velocity * time);
		return transform.position + _velocity * time;
	}


	private void Update()
	{
		if (_moveTarget == null)
			return;

		_velocity = _direction.normalized * _speed;
		float frameDistance = _speed * Time.deltaTime;
		if (_direction.magnitude > frameDistance)
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

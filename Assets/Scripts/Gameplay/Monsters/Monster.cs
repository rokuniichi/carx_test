using UnityEngine;
using System;
using System.Collections.Generic;

public class Monster : MonoBehaviour, IKillable, IDamagable, IPredictable {

	public Action<Transform> OnKill { get; set; }
	public Vector3 Velocity => _direction.normalized * _speed;

	private Queue<Transform> _path;
	private Transform        _moveTarget;

	private float _health;
	private float _speed;

	private Vector3 _direction => _moveTarget.transform.position - transform.position;

    public void Init(MonsterData monsterData, List<Transform> path)
    {
		_health     = monsterData.MaxHealth;
		_speed      = monsterData.Speed;
		_path       = new Queue<Transform>(path);
		_moveTarget = _path.Dequeue();
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

		float frameDistance = _speed * Time.deltaTime;
		if (_direction.magnitude > frameDistance)
		{
			transform.position += Velocity * Time.deltaTime;
			transform.forward = Velocity;
		}
		else
		{
			if (_path.Count == 0)
				KillSelf();
			else
				_moveTarget = _path.Dequeue();
		}
	}
}

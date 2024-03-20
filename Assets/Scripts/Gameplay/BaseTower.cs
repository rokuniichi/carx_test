using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class BaseTower : MonoBehaviour 
{
	[SerializeField] protected Transform shootingPoint;

	protected Transform _currentTarget;
	protected ProjectileData _projectileData;

	private TowerData          _towerData;
	private SphereCollider     _collider;
	private Timer              _cooldownTimer;
	private HashSet<Transform> _allTargets;

	private bool _isInit;

	public void Init(TowerData towerData)
    {
		_towerData = towerData;
		_projectileData = _towerData.ProjectileData;
		_collider = GetComponent<SphereCollider>();
		_collider.radius = _towerData.Range;
		_cooldownTimer = new Timer();
		_allTargets = new HashSet<Transform>();
		_isInit = true;
	}

	protected abstract void Fire();

	private void UpdateTarget()
    {
		if (_currentTarget != null) return;
		float currentDistance = _towerData.Range;
		Transform result = null;
        foreach (Transform target in _allTargets)
        {
			float distance = Vector3.Distance(transform.position, target.position);
			if (distance <= currentDistance) 
				result = target;
        }

		_currentTarget = result;
    }

	private void AddTarget(Transform target)
    {
		if (_currentTarget == null)
			_currentTarget = target;
		_allTargets.Add(target);

		target.GetComponent<IKillable>().OnKill += RemoveTarget;
	}

	private void RemoveTarget(Transform target)
    {
		_allTargets.Remove(target);
		if (_currentTarget == target)
		{
			_currentTarget = null;
			UpdateTarget();
		}

		target.GetComponent<IKillable>().OnKill -= RemoveTarget;
	}

	private void Update()
	{
		if (!_isInit) return;
		_cooldownTimer.Tick(Time.deltaTime);
		if (_cooldownTimer.CurrentDuration <= 0f && _currentTarget != null)
		{
			Fire();
			_cooldownTimer.Start(_towerData.Cooldown);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_isInit) return;
		if (other.gameObject.layer != _towerData.TargetLayer) return;
		AddTarget(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
		if (!_isInit) return;
		if (other.gameObject.layer != _towerData.TargetLayer) return;
		RemoveTarget(other.transform);
    } 
}

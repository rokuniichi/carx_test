using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
	protected abstract void OnSetTarget();

	private void UpdateTarget()
    {
		if (_currentTarget != null) return;
		float currentDistance = _towerData.Range;
		Transform result = null;
        foreach (Transform target in _allTargets)
        {
			float distance = Vector3.Distance(transform.position, target.position);
			if (distance <= currentDistance)
            {
				result = target;
				currentDistance = distance;
			}
        }

		SetTarget(result);
    }

	private void SetTarget(Transform target)
    {
		_currentTarget = target;
		OnSetTarget();
    }

	private void AddTarget(Transform target)
    {
		_allTargets.Add(target);
		if (_currentTarget == null)
			UpdateTarget();

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

	private bool CheckTargetLayer(int layer)
    {
		return _towerData.TargetLayers.value == (_towerData.TargetLayers.value | (1 << layer));

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
		if (!CheckTargetLayer(other.gameObject.layer)) return;
		AddTarget(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
		if (!_isInit) return;
		if (!CheckTargetLayer(other.gameObject.layer)) return;
		RemoveTarget(other.transform);
    } 
}

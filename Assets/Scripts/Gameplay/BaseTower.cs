using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ITargetSystem))]
public abstract class BaseTower : MonoBehaviour 
{
	[SerializeField] protected Transform shootingPoint;
	[SerializeField] private TimerBehaviour cooldownTimer;
	[SerializeField] private UnityEvent onFire;

	public ProjectileData ProjectileData => _towerData.ProjectileData;

	protected Transform _currentTarget;

	private TowerData          _towerData;
	private ITargetSystem      _targetSystem;

	private bool _onCooldown;

    private void Start()
    {
		_targetSystem = GetComponent<ITargetSystem>();
    }

    public void Init(TowerData towerData)
    {
		_towerData = towerData;
		_onCooldown = false;

		_targetSystem.Init(_towerData.Range, _towerData.TargetLayers);

		cooldownTimer.SetDuration(_towerData.Cooldown);
	}

	public void SetCooldown(bool state)
    {
		_onCooldown = state;
    }

	protected abstract void OnFire();

	public void TryFire()
	{
		if (_onCooldown) return;
		OnFire();
		onFire?.Invoke();
	}
}

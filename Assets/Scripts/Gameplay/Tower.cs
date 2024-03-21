using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ITargetSystem), typeof(IProjectileSpawner))]
public class Tower : MonoBehaviour 
{
	[SerializeField] protected Transform shootingPoint;
	[SerializeField] private TimerBehaviour cooldownTimer;
	[SerializeField] private UnityEvent onFire;

	public ProjectileData ProjectileData => _towerData.ProjectileData;

	protected Transform _currentTarget;

	private TowerData          _towerData;
	private ITargetSystem      _targetSystem;
	private IProjectileSpawner _projectileSpawner;

	private bool _onCooldown;

    private void Start()
    {
		_targetSystem = GetComponent<ITargetSystem>();
		_projectileSpawner = GetComponent<IProjectileSpawner>();
    }

    public void Init(TowerData towerData)
    {
		_towerData = towerData;
		_onCooldown = false;

		_projectileSpawner.Init(_towerData.ProjectileData, shootingPoint);
		_targetSystem.Init(_towerData.Range, _towerData.TargetLayers);

		cooldownTimer.SetDuration(_towerData.Cooldown);
	}

	public void SetCooldown(bool state)
    {
		_onCooldown = state;
    }

	public void TryFire()
	{
		if (_onCooldown) return;
		onFire?.Invoke();
	}
}

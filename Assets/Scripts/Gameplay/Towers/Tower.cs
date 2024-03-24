using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour 
{
	[SerializeField] private OnTowerInit onTowerInit;
	[SerializeField] private UnityEvent  onFire;

	private TowerData _towerData;
	private bool      _onCooldown;

    public void Init(TowerData towerData)
    {
		_towerData = towerData;
		_onCooldown = false;
		onTowerInit?.Invoke(_towerData);
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

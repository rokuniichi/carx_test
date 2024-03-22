using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    // Spaghetti class for prototyping/debugging
    [SerializeField] private Camera    _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private GameObject _currentBase;
    private Tower      _currentWeapon;
    private TowerData  _currentData;
    public void SelectTower(TowerData towerData)
    {
        GameObject towerBase = PoolManager.Instance.Create(towerData.BasePrefab);
        GameObject towerWeapon = PoolManager.Instance.Create(towerData.WeaponPrefab);
        towerBase.transform.SetParent(transform);
        towerWeapon.transform.SetParent(towerBase.transform);
        towerWeapon.transform.localPosition = Vector3.zero + towerData.WeaponPlacementOffset;
        towerWeapon.GetComponent<SphereCollider>().enabled = false;

        _currentBase = towerBase;
        _currentWeapon = towerWeapon.GetComponent<Tower>();
        _currentData = towerData;
    }

    private void Update()
    {
        if (_currentBase == null) return;

        Ray ray = _mainCamera.ScreenPointToRay(KeyboardInput.MousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, ~_layerMask))
            _currentBase.transform.position = hit.point;

        if (KeyboardInput.LMBClick)
        {
            _currentWeapon.Init(_currentData);
            _currentWeapon.GetComponent<SphereCollider>().enabled = true;
            _currentBase = null;
            _currentWeapon = null;
            _currentData = null;
        }
    }
}

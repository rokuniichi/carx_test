using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Spaghetti class for prototyping/debugging
    [SerializeField] Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private GameObject _currentBase;
    private BaseTower _currentWeapon;
    private TowerData _currentData;
    public void SelectTower(TowerData towerData)
    {
        GameObject towerBase = Instantiate(towerData.BasePrefab, gameObject.transform.position, towerData.BasePrefab.transform.rotation, gameObject.transform);
        GameObject towerWeapon = Instantiate(towerData.WeaponPrefab, towerBase.transform);
        towerWeapon.transform.localPosition = Vector3.zero + towerData.WeaponPlacementOffset;
        _currentBase = towerBase;
        _currentWeapon = towerWeapon.GetComponent<BaseTower>();
        _currentData = towerData;
    }

    private void Update()
    {
        if (_currentBase == null) return;
        Ray ray = _mainCamera.ScreenPointToRay(KeyboardInput.MousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, ~_layerMask))
        {
            _currentBase.transform.position = hit.point;
        }

        if (KeyboardInput.LMBClick)
        {
            _currentWeapon.Init(_currentData);
            _currentBase = null;
            _currentWeapon = null;
            _currentData = null;
        }
    }
}

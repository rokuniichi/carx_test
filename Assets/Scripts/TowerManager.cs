using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public void SpawnTower(TowerData towerData)
    {
        GameObject towerBase = Instantiate(towerData.BasePrefab);
        GameObject towerWeapon = Instantiate(towerData.WeaponPrefab, towerBase.transform);
        towerWeapon.transform.localPosition = Vector3.zero + towerData.WeaponPlacementOffset;
    }
}

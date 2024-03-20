using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private GameObject ConstructTower(TowerData towerData)
    {
        GameObject towerBase = Instantiate(towerData.BasePrefab);
        GameObject towerWeapon = Instantiate(towerData.WeaponPrefab, towerBase.transform);
        towerWeapon.transform.localPosition = Vector3.zero + towerData.WeaponPlacementOffset;
        return towerBase;
    }
}

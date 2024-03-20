using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private GameObject currentPlacement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlacement != null)
            gridManager.ProjectOntoSelectedCell(currentPlacement);
    }

    public void SelectTowerToPlace(TowerData towerData)
    {
        if (currentPlacement == null)
            currentPlacement = ConstructTower(towerData);
    }

    private GameObject ConstructTower(TowerData towerData)
    {
        GameObject towerBase = Instantiate(towerData.BasePrefab);
        GameObject towerWeapon = Instantiate(towerData.WeaponPrefab, towerBase.transform);
        towerWeapon.transform.localPosition = Vector3.zero + towerData.WeaponPlacementOffset;
        return towerBase;
    }
}

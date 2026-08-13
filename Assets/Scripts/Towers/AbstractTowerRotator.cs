using UnityEngine;

public abstract class AbstractTowerRotator : MonoBehaviour
{
    protected TowerData towerData;
    protected int Range;

    protected virtual void OnEnable()
    {

        TowerEvents.OnTowerBuilt += SetTowerData;
        TowerEvents.OnTowerUpgraded += SetDataAfterUpgrade;
    }

    protected virtual void OnDisable()
    {

        TowerEvents.OnTowerBuilt -= SetTowerData;
        TowerEvents.OnTowerUpgraded -= SetDataAfterUpgrade;
    }

    protected virtual void SetTowerData(TowerData td, GameObject sender)
    {
        if (sender != gameObject) return;
        Range = td.Range;

    }

    protected virtual void SetDataAfterUpgrade(Tower tower, GameObject sender)
    {
        if (gameObject != sender) return;
        towerData = tower.towerData;
        Range = towerData.Range + (10 * (tower.Tier - 1));

    }
}

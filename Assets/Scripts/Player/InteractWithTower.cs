using UnityEditor.PackageManager;
using UnityEngine;

public class InteractWithTower : MonoBehaviour
{
    [SerializeField] LayerMask TowerMask;
    [SerializeField] TowerInfoUIController TowerInfo;
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f,
            TowerMask))
        {
            
            Tower tower = hitInfo.collider.GetComponent<Tower>();
            if(tower != null)
            {
                
                if (Input.GetKeyDown(KeyCode.H))
                {
                    SellTower(tower);
                }
                else if (Input.GetKeyDown(KeyCode.U) && PlayerGemPickUp.GemCounter >= 5 * tower.Tier)
                {
                    
                    UpgradeTower(tower);
                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    TowerAttack ta = tower.GetComponentInChildren<TowerAttack>();
                    TowerUIModell ui = new(tower, ta);
                    
                    if (!TowerInfo.IsOpened)
                    {
                        TowerInfo.Show(ui);
                    }
                    else
                    {
                        TowerInfo.Hide();
                    }
                    
                }
                
            }
            

            
        }
        else if(TowerInfo.IsOpened)
        {
            TowerInfo.Hide();
        }

    }

    void SellTower(Tower tower)
    {
        TowerEvents.TowerSold(tower.Cost);
        Destroy(tower.gameObject);
    }

    void UpgradeTower(Tower tower)
    {
        TowerEvents.GemSpent(5 * tower.Tier);
        tower.SetHealth((tower.Health / 2) + tower.Health + 5 * tower.Tier);
        tower.IncreaseTier();
        
        tower.UpgradeVisual();
        TowerEvents.TowerUpgraded(tower, tower.transform.GetChild(0).gameObject);
    }


}

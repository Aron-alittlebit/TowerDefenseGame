using UnityEditor.PackageManager;
using UnityEngine;

public class InteractWithTower : MonoBehaviour
{
    [SerializeField] LayerMask TowerMask;
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
                    Debug.Log("aha");
                }
            }

            
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
        tower.SetHealth(tower.Health + 5 * tower.Tier);
        tower.IncreaseTier();
        
        tower.UpgradeVisual();
        TowerEvents.TowerUpgraded(tower, tower.transform.GetChild(0).gameObject);
    }


}

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
            Debug.Log(tower.Tier);

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
                Debug.Log(tower.Tier);
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
        
        TowerEvents.TowerUpgraded(tower, tower.gameObject);
    }


}

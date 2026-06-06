using UnityEditor.PackageManager;
using UnityEngine;

public class InteractWithTower : MonoBehaviour
{
    [SerializeField] LayerMask TowerMask;
    void Update()
    {
        //if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo1, 10f,
        //    TowerMask) && Input.GetKey(KeyCode.H))
        //{
        //    Tower tower = hitInfo1.collider.GetComponent<Tower>();
        //    if (tower != null)
        //    {


        //    }
        //}
        //else if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo2, 10f
        //    , TowerMask) && Input.GetKey(KeyCode.U))
        //{
        //    Tower tower = hitInfo2.collider.GetComponent<Tower>();
        //    if (tower != null && PlayerGemPickUp.GemCounter >= 5 * tower.Tier)
        //    {

                

        //    }
        //}

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f,
            TowerMask))
        {
            Tower tower = hitInfo.collider.GetComponent<Tower>();

            if (Input.GetKey(KeyCode.H))
            {
                SellTower(tower);
            }
            else if (Input.GetKey(KeyCode.U) && PlayerGemPickUp.GemCounter >= 5 * tower.Tier)
            {
                UpgradeTower(tower);
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
        tower.IncreaseTier();
        TowerEvents.TowerUpgraded(tower);
    }


}

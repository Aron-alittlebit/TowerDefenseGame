using UnityEngine;

public class UpgradeTower : MonoBehaviour
{

    
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f)
            && Input.GetKey(KeyCode.U))
        {
            Tower tower = hitInfo.collider.GetComponent<Tower>();
            if (tower != null && PlayerGemPickUp.GemCounter >= 5 * tower.Tier)
            {

                TowerEvents.GemSpent(5 * tower.Tier);
                tower.IncreaseTier();
                TowerEvents.TowerUpgraded(tower.Tier);

            }
        }

        //if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f)
        //    )
        //{
        //    Debug.Log(hitInfo.collider.name);
        //}
    }
}

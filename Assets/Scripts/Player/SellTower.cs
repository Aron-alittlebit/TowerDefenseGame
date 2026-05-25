using UnityEngine;

public class SellTower : MonoBehaviour
{
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f)
            && Input.GetKey(KeyCode.H))
        {
            Tower tower = hitInfo.collider.GetComponent<Tower>();
            if (tower != null)
            {

                TowerEvents.TowerSold(tower.Cost);
                Destroy(tower.gameObject);
            }
        }

    }
}

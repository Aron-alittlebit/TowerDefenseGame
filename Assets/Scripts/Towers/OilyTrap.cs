using UnityEngine;

public class OilyTrap : MonoBehaviour
{
    [SerializeField] float Duration;
    [SerializeField] float SlowedDownSpeed;

    private void OnTriggerEnter(Collider other)
    {
        Entity enemy = other.GetComponent<Entity>();
        if(enemy != null)
        {
            enemy.GetComponent<EntityMove>().ApplyOil(Duration, SlowedDownSpeed);
        }
    }

    private void OnEnable()
    {
        TowerEvents.OnTowerUpgraded += UpgradeOil;
    }

    private void OnDisable()
    {
        TowerEvents.OnTowerUpgraded -= UpgradeOil;
        
    }

    void UpgradeOil(Tower tower, GameObject sender)
    {
        if (sender != transform.GetChild(0).gameObject) return;

        Duration += 0.2f * tower.Tier;
        SlowedDownSpeed -= 0.2f * tower.Tier;
    }
}

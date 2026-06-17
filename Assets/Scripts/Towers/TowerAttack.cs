
using UnityEngine;
using UnityEngineInternal;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class TowerAttack : MonoBehaviour
{
    
    public Transform FirePoint;
    protected float currentCoolDown;
    protected TowerData towerData;
    protected int Damage;
    protected float Range;
    protected float CoolDown;
    

    protected virtual void Start()
    {

        currentCoolDown = 0;
        
    }
    protected virtual void OnEnable()
    {
        GunEvents.OnTowerAttack += Attack;
        TowerEvents.OnTowerBuilt += SetTowerData;
        TowerEvents.OnTowerUpgraded += SetDataAfterUpgrade;
    }

    protected virtual void OnDisable()
    {
        GunEvents.OnTowerAttack -= Attack;
        TowerEvents.OnTowerBuilt -= SetTowerData;
        TowerEvents.OnTowerUpgraded -= SetDataAfterUpgrade;
    }

    
    protected virtual void Update()
    {
        currentCoolDown -= Time.deltaTime;
        
    }

    protected virtual void Attack(GameObject sender)
    {
        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {
            if (Physics.Raycast(FirePoint.position, FirePoint.forward, out RaycastHit hitInfo,
                Range, Tower.Instance.EntityLayer))
            {
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                
                enemy.TakeDamage(Damage);
            }
            
            currentCoolDown = CoolDown;
        }
    }

    protected virtual void SetTowerData(TowerData td)
    {
        towerData = td;
        Range = towerData.Range;
        Damage = towerData.Damage;
        CoolDown = towerData.CoolDown;
        currentCoolDown = CoolDown;
    }

    protected virtual void SetDataAfterUpgrade(Tower tower)
    {
        Damage += 2 * tower.Tier;
        Range += 2 * tower.Tier;
        CoolDown -= 0.1f * tower.Tier;
        currentCoolDown = CoolDown;
        tower.SetHealth(tower.Health + 5 * tower.Tier);
    }




}

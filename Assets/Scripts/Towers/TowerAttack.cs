
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngineInternal;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class TowerAttack : MonoBehaviour
{
    
    public Transform FirePoint;
    protected float currentCoolDown;
    protected TowerData towerData;
    protected int damage;
    protected int range;
    protected float coolDown;
   
    public int Damage => damage;
    public int Range => range;
    public float CoolDown => coolDown;
    

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
            if (Physics.SphereCast(FirePoint.position, 0.5f ,FirePoint.forward, out RaycastHit hitInfo,
                range, Tower.Instance.EntityLayer))
            {
                
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                
                enemy.TakeDamage(damage);
                if(enemy.Health <= 0)
                {
                    TowerEvents.TowerKilledEntity(gameObject);
                }
            }
            
            currentCoolDown = coolDown;
        }
    }

    protected virtual void SetTowerData(TowerData td, GameObject sender)
    {
        
        if (sender != gameObject) return;
        towerData = td;
        range = towerData.Range;
        damage = towerData.Damage;
        coolDown = towerData.CoolDown;
        currentCoolDown = coolDown;
    }

    protected virtual void SetDataAfterUpgrade(Tower tower, GameObject sender)
    {
        
        if (gameObject != sender) return;
        towerData = tower.towerData;
        damage = towerData.Damage + (10 * (tower.Tier));
        range = towerData.Range + (10 * (tower.Tier));
        
        coolDown = towerData.CoolDown -  (0.1f * tower.Tier);
        if (coolDown <= 0)
            coolDown = 0.1f;

        currentCoolDown = coolDown;
 
    }

}

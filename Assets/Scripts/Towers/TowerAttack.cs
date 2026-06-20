
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class TowerAttack : MonoBehaviour
{
    
    public Transform FirePoint;
    protected float currentCoolDown;
    protected TowerData towerData;
    protected int Damage;
    protected int Range;
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
        Debug.Log($"{Damage}, {Range}, {currentCoolDown}, {CoolDown} {FirePoint != null}");
        //Debug.Log(Damage);
        
    }

    protected virtual void Attack(GameObject sender)
    {
        //Debug.Log(FirePoint != null);
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

    protected virtual void SetTowerData(TowerData td, GameObject sender)
    {
        //Debug.Log(sender != gameObject);
        if (sender != gameObject) return;
        towerData = td;
        Range = towerData.Range;
        Damage = towerData.Damage;
        CoolDown = towerData.CoolDown;
        currentCoolDown = CoolDown;
    }

    protected virtual void SetDataAfterUpgrade(Tower tower, GameObject sender)
    {
        
        if (gameObject != sender) return;
        towerData = tower.towerData;
        Damage = towerData.Damage + (10 * (tower.Tier - 1));
        Range = towerData.Range + (10 * (tower.Tier - 1));
        transform.GetComponent<TowerRotator>().SetRange(Range);
        CoolDown -= 0.1f * tower.Tier;
        currentCoolDown = CoolDown;


        
    }





}

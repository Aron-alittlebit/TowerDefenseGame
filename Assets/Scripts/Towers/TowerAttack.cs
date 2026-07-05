
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngineInternal;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class TowerAttack : MonoBehaviour
{
    
    public Transform FirePoint;
    protected float currentCoolDown;
    protected TowerData towerData;
    protected int damage;
    protected int range;
    protected float coolDown;
    protected DamageType damageType;
    int maxBounces = 5;
   
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
            if (Physics.Raycast(FirePoint.position, FirePoint.forward, out RaycastHit hitInfo,
                range, Tower.Instance.EntityLayer))
            {
                SoundManager.instance.PlaySound(towerData.ShootingSound, transform, 30f);
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                if(enemy != null)
                {
                    
                    if(damageType == DamageType.Electric)
                    {
                        StartElectricChain(enemy);
                    }
                    else if(damageType == DamageType.Normal)
                    {
                        enemy.TakeDamage(damage);
                    }

                    if (enemy.Health <= 0)
                    {
                        TowerEvents.TowerKilledEntity(gameObject);
                    }
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
        damageType = towerData.DamageType;
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
        damageType = towerData.DamageType;
 
    }

    void StartElectricChain(Entity FirstTarget)
    {
        List<Entity> alreadyHit = new();
        StartCoroutine(ElectricChain(alreadyHit, maxBounces, damage, FirstTarget));
        
    }

    IEnumerator ElectricChain(List<Entity> alreadyHit, int BouncesLeft, int damage, Entity Target)
    {
        if (BouncesLeft == 0 || Damage <= 0 || Target == null) yield break;
        
        Target.TakeDamage(damage);
        alreadyHit.Add(Target);
        yield return new WaitForSeconds(0.1f);
        Entity nextTarget = FindClosestEnemy(Target.transform.position, alreadyHit);

        if (nextTarget != null)
            StartCoroutine(ElectricChain(alreadyHit, BouncesLeft - 1, damage - 5, nextTarget));
    }

    Entity FindClosestEnemy(Vector3 pos, List<Entity> alreadyHit)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, 5, Tower.Instance.EntityLayer);
        Entity closest = null;
        float minDst = float.MaxValue;

        foreach(var col in colliders)
        {
            Entity enemy = col.GetComponent<Entity>();
            if(enemy != null && !alreadyHit.Contains(enemy))
            {
                float dst = Vector3.Distance(pos, enemy.transform.position);
                if(dst < minDst)
                {
                    minDst = dst;
                    closest = enemy;
                }
            }
        }

        return closest;
    }

}

public enum DamageType
{
    Normal,
    Electric,
    Fire
}

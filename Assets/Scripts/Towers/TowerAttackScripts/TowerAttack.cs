
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
    //[SerializeField] BulletScript Bullet;
    public Transform FirePoint;
    protected float currentCoolDown;
    protected TowerData towerData;
    protected int damage;
    protected int range;
    protected float coolDown;
    protected DamageType damageType;

    int ogDamage;
    int ogRange;
    float ogCoolDown;


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
            if (Physics.Raycast(FirePoint.position, FirePoint.forward,
                out RaycastHit hitInfo, range, Tower.Instance.EntityLayer))
            {
                SoundManager.instance.PlaySound(towerData.ShootingSound, transform, 30f);
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                if (enemy != null)
                {


                    //if (damageType == DamageType.Normal)
                    //{
                    //    BulletScript bullet = Instantiate(Bullet, FirePoint.position
                    //        , Quaternion.identity);
                    //    bullet.SetData(towerData,
                    //        enemy.transform.position - FirePoint.position);
                    //}
                    enemy.TakeDamage(Damage);
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
        //damageType = towerData.DamageType;
 
    }

    public void SetDataForBoost(float boostRate)
    {
        ogDamage = damage;
        ogCoolDown = coolDown;
        ogRange = range;

        damage = (int)Mathf.Round(damage * boostRate);
        range = (int)Mathf.Round(range * boostRate);
        coolDown = (int)Mathf.Round(coolDown * (2.3f-boostRate));

    }

    public void SetDataBackAfterBoost()
    {
        damage = ogDamage;
        coolDown = ogCoolDown;
        range = ogRange;
    }


}

public enum DamageType
{
    Normal,
    Electric,
    Fire
}

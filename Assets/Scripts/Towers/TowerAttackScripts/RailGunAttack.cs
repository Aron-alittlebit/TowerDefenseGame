using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class RailGunAttack : TowerAttack
{
    [Header("Railgun Visual Settings")]
    [SerializeField] private RailGunBeamVisual beamVisualPrefab;
    [SerializeField] private float chargeDuration = 0.8f;
    [SerializeField] private float laserDisplayDuration = 0.3f;
    protected override void OnEnable()
    {
        GunEvents.OnTowerAttack += StartAttack;
        TowerEvents.OnTowerBuilt += SetTowerData;
        TowerEvents.OnTowerUpgraded += SetDataAfterUpgrade;
    }

    protected override void OnDisable()
    {
        GunEvents.OnTowerAttack -= StartAttack;
        TowerEvents.OnTowerBuilt -= SetTowerData;
        TowerEvents.OnTowerUpgraded -= SetDataAfterUpgrade;
    }
    protected IEnumerator RailGunAttackWithSound()
    {
        yield return new WaitForSeconds(chargeDuration);
            RaycastHit[] colliders = Physics.SphereCastAll(FirePoint.position, 2f, FirePoint.forward
                , range, Tower.Instance.EntityLayer);
            SoundManager.instance.PlaySound(towerData.ShootingSound, transform, 30f);
        if (beamVisualPrefab != null)
        {
            RailGunBeamVisual effect = Instantiate(beamVisualPrefab, Vector3.zero,
                Quaternion.identity);

            Vector3 beamStart = FirePoint.position;
            Vector3 beamEnd = FirePoint.position + (FirePoint.forward * range);

            effect.PlayVisual(beamStart, beamEnd, chargeDuration,
                laserDisplayDuration);
        }
        yield return new WaitForSeconds(0.9f);
            if (colliders.Length > 0)
            {

                foreach(var col in colliders)
                {
                    Entity enemy = col.collider.GetComponent<Entity>();
                    if (enemy != null)
                    {
                        
                        enemy.TakeDamage(damage);
                        if (enemy.Health <= 0)
                        {
                            TowerEvents.TowerKilledEntity(gameObject);
                        }
                    }
                }

                

            }

            currentCoolDown = coolDown;
        
    }

    void StartAttack(GameObject sender)
    {
        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {
            

            
            
            StartCoroutine(RailGunAttackWithSound());

            currentCoolDown = coolDown;
        }
    }
}



using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngineInternal;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class ElectricTowerAttack : TowerAttack
{
    
    int maxBounces = 5;

    [Header("Visual Effects")]
    [SerializeField] private LightningBoltEffect lightningPrefab;
    [SerializeField] private Color lightningColor = Color.cyan;
    [SerializeField] private float effectDuration = 0.1f;
    


    protected override void Update()
    {
        base.Update();
        Debug.Log(currentCoolDown);
    }
    protected override void Attack(GameObject sender)
    {

        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {
            if (Physics.Raycast(FirePoint.position, FirePoint.forward,
                out RaycastHit hitInfo, range, Tower.Instance.EntityLayer))
            {
                SoundManager.instance.PlaySound(towerData.ShootingSound, transform, 30f);
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                if (enemy != null && !enemy.HasDied)
                {
                    
                    StartElectricChain(enemy);
                    if (enemy.Health <= 0)
                    {
                        TowerEvents.TowerKilledEntity(gameObject);
                    }
                    currentCoolDown = coolDown;
                }

            }

            
        }
    }

    

    void StartElectricChain(Entity FirstTarget)
    {
        List<Entity> alreadyHit = new();
        StartCoroutine(ElectricChain(alreadyHit, maxBounces, damage, FirstTarget,
            FirePoint.position));

    }

    IEnumerator ElectricChain(List<Entity> alreadyHit, int BouncesLeft, int damage,
        Entity Target, Vector3 originPosition)
    {
        if (BouncesLeft == 0 || damage <= 0 || Target == null) yield break;

        if (lightningPrefab != null)
        {
            LightningBoltEffect bolt = Instantiate(lightningPrefab, Vector3.zero,
                Quaternion.identity);

            Vector3 targetCenter = Target.transform.position + Vector3.up;
            bolt.SetupLine(originPosition, targetCenter, effectDuration, lightningColor);
        }

        Target.TakeDamage(damage);
        alreadyHit.Add(Target);
        yield return new WaitForSeconds(0.1f);
        Entity nextTarget = FindClosestEnemy(Target.transform.position, alreadyHit);

        if (nextTarget != null)
            StartCoroutine(ElectricChain(alreadyHit, BouncesLeft - 1, damage - 5,
                nextTarget, Target.transform.position + Vector3.up));
    }

    Entity FindClosestEnemy(Vector3 pos, List<Entity> alreadyHit)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, 10, Tower.Instance.EntityLayer);
        Entity closest = null;
        float minDst = float.MaxValue;

        foreach (var col in colliders)
        {
            Entity enemy = col.GetComponent<Entity>();
            if (enemy != null && !alreadyHit.Contains(enemy))
            {
                float dst = Vector3.Distance(pos, enemy.transform.position);
                if (dst < minDst)
                {
                    minDst = dst;
                    closest = enemy;
                }
            }
        }

        return closest;
    }



}



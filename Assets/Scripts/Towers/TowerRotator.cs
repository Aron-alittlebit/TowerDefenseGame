using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotator : AbstractTowerRotator
{
    [SerializeField] protected Transform Pivotpoint;
    [SerializeField] float rotationSpeed = 10f;
    
    [SerializeField] float RotationMinLimitY;
    [SerializeField] float RotationMaxLimitY;
    [SerializeField] protected float RotationLimitX;
    
    
    Entity? target = null;
    
    (float,float,float) OriginalRotaion;
    
    
    
    protected virtual void Start()
    {
        OriginalRotaion = (Pivotpoint.localEulerAngles.x, 
            Pivotpoint.localEulerAngles.y, Pivotpoint.localEulerAngles.z);
    }
    

    

    protected void Update()
    {
        
        float originalDistance = 0f;

        target = FindClosestEnemy();
        
        if (target != null)
        {
            originalDistance = Vector3.Distance(transform.position,
                target.transform.position);
            RotateTower(target.transform.position, originalDistance);
            
            GunEvents.TowerAttack(gameObject);
        }
        else
        {
           Pivotpoint.localRotation = Quaternion.Slerp(
                Pivotpoint.localRotation,
                Quaternion.Euler(OriginalRotaion.Item1, 
                OriginalRotaion.Item2, OriginalRotaion.Item3),
                5 * Time.deltaTime);
        }

        if (target != null && Vector3.Distance(transform.position, 
            target.transform.position)
            > Range)
        {
            target = null;
        }
            
    }

    Entity? FindClosestEnemy()
    {
        if (Tower.Instance == null || Range == 0) return null;
        float min = float.MaxValue;
        Entity target = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range,
            Tower.Instance.EntityLayer);

        foreach(var collider in colliders)
        {
            Entity enemy = collider.GetComponent<Entity>();
            if(enemy != null && !enemy.HasDied)
            {
                float dst = Vector3.Distance(transform.position, enemy.transform.position);
                if (dst < min )
                {
                    target = enemy;
                    min = dst;
                }
            }
            
        }

        return target;
    }


    void RotateTower(Vector3 enemyPos, float steps)
    {
        Vector3 directionToEnemy = enemyPos - Pivotpoint.position;
        directionToEnemy.y = 0f;
        if (directionToEnemy == Vector3.zero) return;

        Quaternion targetRotation = 
            Quaternion.LookRotation(Pivotpoint.parent.InverseTransformDirection(directionToEnemy));

        Vector3 targetEuler = targetRotation.eulerAngles;
        float min = Mathf.Min(0f,RotationLimitX);
        float max = Mathf.Max(0f,RotationLimitX);
        targetEuler.x = Mathf.Clamp(Mathf.Lerp(max, min, steps / Range), min, max);


        targetRotation = Quaternion.Euler(targetEuler);
        float targetY = targetRotation.eulerAngles.y;
        if (targetY < RotationMinLimitY || targetY > RotationMaxLimitY)
        {
            targetRotation = Quaternion.Euler(
                OriginalRotaion.Item1,
                OriginalRotaion.Item2,
                Pivotpoint.localEulerAngles.z);
        }

        Pivotpoint.localRotation = Quaternion.Slerp(
            Pivotpoint.localRotation,
            targetRotation,
            Time.deltaTime * rotationSpeed);


    }

    

    


}

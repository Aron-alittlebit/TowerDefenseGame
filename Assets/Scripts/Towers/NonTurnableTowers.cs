using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class NonTurnableTowers : TowerRotator
{
    [SerializeField] Transform FirePoint;
    [SerializeField] bool CanTurnXAxis;
    float originalX;

    protected override void Start()
    {
        originalX = Pivotpoint.localEulerAngles.x;
    }

    

    protected override void Update()
    {
        if (CanTurnXAxis)
        {
            if(Physics.Raycast(FirePoint.position, FirePoint.forward, out RaycastHit hitInfo
                , Range, Tower.Instance.EntityLayer))
            {
                RotateTower(hitInfo.collider.transform.position,
                Vector3.Distance(hitInfo.collider.transform.position, Pivotpoint.position));
                GunEvents.TowerAttack(gameObject);
            }
            
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Range,
            Tower.Instance.EntityLayer);
            foreach (var collider in colliders)
            {
                GunEvents.TowerAttack(gameObject);
                break;

            }
        }


        
    }

    void RotateTower(Vector3 enemyPos, float steps)
    {
        float targetX = originalX + Mathf.Lerp(0f, RotationLimitX, steps / Range);
        float clampedX = Mathf.Clamp(targetX,
            originalX + Mathf.Min(0f, RotationLimitX),
            originalX + Mathf.Max(0f, RotationLimitX));

        Quaternion targetRotation = Quaternion.Euler(
            clampedX,
            Pivotpoint.localEulerAngles.y,
            Pivotpoint.localEulerAngles.z);

        Pivotpoint.localRotation = Quaternion.Slerp(
            Pivotpoint.localRotation,
            targetRotation,
            Time.deltaTime * 5f);
    }

    
}

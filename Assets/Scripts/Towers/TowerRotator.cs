using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotator : MonoBehaviour
{
    [SerializeField] Transform Pivotpoint;
    [SerializeField] float rotationSpeed = 90f;
    [SerializeField] TowerData towerData;
    float Range;
    float closest = float.MaxValue;
    Entity target = null;
    Dictionary<int, float> EntitiesDistance = new Dictionary<int, float>();
    (float,float,float) OriginalRotaion;
    
    
    
    private void Start()
    {
        Range = towerData.Range;
        OriginalRotaion = (Pivotpoint.localEulerAngles.x, 
            Pivotpoint.localEulerAngles.y, Pivotpoint.localEulerAngles.z);
    }
    private void OnEnable()
    {
        EntitiesEvent.OnEntityDeath += RemoveEntityFromDictionary;
        
    }

    private void OnDisable()
    {
        EntitiesEvent.OnEntityDeath -= RemoveEntityFromDictionary;
        
    }

    

    void Update()
    {
        
        float originalDistance = 0f;
        
        if (target != null)
        {
            originalDistance = Vector3.Distance(transform.position, target.transform.position);
            RotateTower(target.transform.position, originalDistance);
            GunEvents.TowerAttack(Range);
            
        }

        else if(target == null)
        {

           Pivotpoint.localRotation = Quaternion.Slerp(
                Pivotpoint.localRotation,
                Quaternion.Euler(OriginalRotaion.Item1, 
                OriginalRotaion.Item2, OriginalRotaion.Item3),
                5 * Time.deltaTime);
        }
            




        Collider[] colliders = Physics.OverlapSphere(transform.position, Range,
            Tower.Instance.EntityLayer);
        foreach (var collider in colliders)
        {
            
            Entity enemy = collider.GetComponent<Entity>();
            int key = enemy.GetInstanceID();


            if (EntitiesDistance.ContainsKey(key))
            {
                EntitiesDistance[key] = Vector3.Distance(transform.position, enemy.transform.position);
                if(EntitiesDistance[key] <= closest)
                {
                    closest = EntitiesDistance[key];
                    target = enemy;
                    
                }
            }
            else
            {
                EntitiesDistance.Add(key,
                    Vector3.Distance(transform.position, enemy.transform.position));
            }

        }



        if (target != null && EntitiesDistance[target.GetInstanceID()] >= closest)
            closest = float.MaxValue;
    }

    void RemoveEntityFromDictionary(int id)
    {
        EntitiesDistance.Remove(id);
    }

    void RotateTower(Vector3 enemyPos, float steps)
    {
        Vector3 directionToEnemy = enemyPos - Pivotpoint.position;
        directionToEnemy.y = 0f;
        if (directionToEnemy == Vector3.zero) return;

        Quaternion targetRotation = 
            Quaternion.LookRotation(Pivotpoint.parent.InverseTransformDirection(directionToEnemy));

        Vector3 targetEuler = targetRotation.eulerAngles;
        targetEuler.x = Mathf.Clamp(Mathf.Lerp(30f, 0f, steps / Range), 0f, 30f);
        targetRotation = Quaternion.Euler(targetEuler);

        float targetY = targetRotation.eulerAngles.y;
        if (targetY < 180)
        {
            targetRotation = Quaternion.Euler(
                OriginalRotaion.Item1,
                OriginalRotaion.Item2,
                Pivotpoint.localEulerAngles.z);
        }

        Pivotpoint.localRotation = Quaternion.Slerp(
            Pivotpoint.localRotation,
            targetRotation,
            Time.deltaTime * 5f);
    }


}

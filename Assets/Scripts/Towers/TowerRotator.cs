using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotator : MonoBehaviour
{
    [SerializeField] protected Transform Pivotpoint;
    [SerializeField] float rotationSpeed = 90f;
    protected TowerData towerData;
    [SerializeField] float RotationMinLimitY;
    [SerializeField] float RotationMaxLimitY;
    [SerializeField] protected float RotationLimitX;
    protected int Range;
    float closest = float.MaxValue;
    Entity target = null;
    Dictionary<int, float> EntitiesDistance = new Dictionary<int, float>();
    (float,float,float) OriginalRotaion;
    
    
    
    protected virtual void Start()
    {
        
        OriginalRotaion = (Pivotpoint.localEulerAngles.x, 
            Pivotpoint.localEulerAngles.y, Pivotpoint.localEulerAngles.z);
    }
    protected virtual void OnEnable()
    {
        EntitiesEvent.OnEntityDeath += RemoveEntityFromDictionary;
        TowerEvents.OnTowerBuilt += SetTowerData;
        TowerEvents.OnTowerUpgraded += SetDataAfterUpgrade;


    }

    protected virtual void OnDisable()
    {
        EntitiesEvent.OnEntityDeath -= RemoveEntityFromDictionary;
        TowerEvents.OnTowerBuilt -= SetTowerData;
        TowerEvents.OnTowerUpgraded -= SetDataAfterUpgrade;


    }

    

    protected virtual void Update()
    {
        //Debug.Log($"Range:{Range}, {name}");
        float originalDistance = 0f;
        
        if (target != null)
        {
            originalDistance = Vector3.Distance(transform.position, target.transform.position);
            RotateTower(target.transform.position, originalDistance);
            
            GunEvents.TowerAttack(gameObject);
            Debug.Log("Shoot");
            
            
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

    //public void SetRange(int range)
    //{
    //    //Debug.Log($"Old range: {Range}");
    //    Range = range;
    //    //Debug.Log($"New range: {Range}");
    //}

    protected virtual void SetTowerData(TowerData td, GameObject sender)
    {
        if (sender != gameObject) return;
        Range = td.Range;
        
    }

    protected virtual void SetDataAfterUpgrade(Tower tower, GameObject sender)
    {
        if (gameObject != sender) return;
        towerData = tower.towerData;
        Range = towerData.Range + (10 * (tower.Tier - 1));

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
            Time.deltaTime * 5f);


    }

    

    


}

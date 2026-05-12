using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotator : MonoBehaviour
{
    [SerializeField] Transform Pivotpoint;
    [SerializeField] float rotationSpeed = 90f;
    public float Radius = 25;
    [SerializeField] LayerMask EntityLayer;
    float closest = float.MaxValue;
    Entity target = null;
    Dictionary<int, float> EntitiesDistance = new Dictionary<int, float>();
    (float,float,float) OriginalRotaion;
    private void Start()
    {
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
        //float currentDistance = 0f;
        if (target != null)
        {
            originalDistance = Vector3.Distance(transform.position, target.transform.position);
            RotateTower(target.transform.position, originalDistance);
            
        }

        else if(target == null)
        {

           Pivotpoint.localRotation = Quaternion.Lerp(
                Pivotpoint.localRotation,
                Quaternion.Euler(OriginalRotaion.Item1, OriginalRotaion.Item2, OriginalRotaion.Item3),
                rotationSpeed * Time.deltaTime);
        }
            




        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, EntityLayer);
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


        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);

        targetRotation.x += steps * Time.deltaTime;

        targetRotation.x = Mathf.Clamp(targetRotation.x, 0, 30);

        if (!(targetRotation.y <= 0 && targetRotation.y >= -1))

        {
            targetRotation.y = -0.707f;
        }

        Debug.Log(targetRotation.y);

        Pivotpoint.rotation = Quaternion.Slerp(

            Pivotpoint.rotation,

            targetRotation,

            Time.deltaTime * 5f);

    }


}

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
    Dictionary<string, float> EntitiesDistance = new Dictionary<string, float>();
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
        if (target != null)
        {
            RotateTower(target.transform.position);
            Debug.Log($"x: {target.transform.position.x}" +
                $"y: {target.transform.position.y}" +
                $"z: {target.transform.position.z}");
            
        }
            

        
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, EntityLayer);
        foreach (var collider in colliders)
        {
            
            Entity enemy = collider.GetComponent<Entity>();
            string key = enemy.name;
            
            
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



        if (target != null && EntitiesDistance[target.name] >= closest)
            closest = float.MaxValue;
    }

    void RemoveEntityFromDictionary(string name)
    {
        EntitiesDistance.Remove(name);
    }

    void RotateTower(Vector3 enemyPos)
    {
        //Pivotpoint.Rotate(0f,rotationSpeed*Time.deltaTime,0f);
        
        Pivotpoint.localRotation = Quaternion.Euler(0f,
            (Mathf.Clamp(OriginalRotaion.Item2 + enemyPos.z, 100, 260)),
            Mathf.Clamp(OriginalRotaion.Item3 - enemyPos.x, -30, 0));
    }


}

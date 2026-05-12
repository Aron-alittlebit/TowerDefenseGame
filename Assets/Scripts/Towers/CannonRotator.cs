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
        
        //Pivotpoint.Rotate(0f,rotationSpeed*Time.deltaTime,0f);
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
                    Debug.Log($"{key} {Vector3.Distance(transform.position, enemy.transform.position)}");
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

    
}

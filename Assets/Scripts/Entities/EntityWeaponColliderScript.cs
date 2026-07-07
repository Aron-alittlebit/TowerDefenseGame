using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeaponColliderScript : MonoBehaviour
{
    [SerializeField] int Damage;
    
    [SerializeField] BoxCollider Collider;
    bool HasHit;
    private void Start()
    {
        Collider.enabled = false;
        HasHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        LivingAbstractClass enemy = other.GetComponent<LivingAbstractClass>();
        

        if (enemy != null && enemy.Health > 0 && HasHit && !enemy.GetComponent<Entity>())
        {
            enemy.TakeDamage(Damage);
            HasHit = false;

        }
    }

    public void SetWeapon(bool value)
    {
        
        Collider.enabled = value;
        HasHit = value;
        
    }
}

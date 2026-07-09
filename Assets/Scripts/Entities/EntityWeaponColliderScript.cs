using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeaponColliderScript : MonoBehaviour
{
    [SerializeField] protected int Damage;

    [SerializeField] BoxCollider Collider;
    protected bool HasHit;
    protected virtual void Start()
    {
        Collider.enabled = false;
        HasHit = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        LivingAbstractClass enemy = other.GetComponent<LivingAbstractClass>();
        

        if (enemy != null && enemy.Health > 0 && HasHit && !enemy.GetComponent<Entity>())
        {
            enemy.TakeDamage(Damage);
            HasHit = false;

        }
    }

    public void EnableWeapon()
    {
        
        Collider.enabled = true;
        HasHit = true;
        
    }

    public void DisableWeapon()
    {

        Collider.enabled = false;
        HasHit = false;

    }
}

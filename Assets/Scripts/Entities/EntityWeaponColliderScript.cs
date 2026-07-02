using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeaponColliderScript : MonoBehaviour
{
    [SerializeField] int Damage;
    
    [SerializeField] BoxCollider Collider;
    private void Start()
    {
        Collider.enabled = false;
    }
    private void OnEnable()
    {
        EntitiesEvent.OnWeaponSet += SetWeapon;
    }

    private void OnDisable()
    {
        EntitiesEvent.OnWeaponSet -= SetWeapon;
    }


    private void OnTriggerEnter(Collider other)
    {
        LivingAbstractClass enemy = other.GetComponent<LivingAbstractClass>();

        if (enemy != null && enemy.Health > 0)
        {
            enemy.TakeDamage(Damage);
        }
    }

    void SetWeapon(bool value)
    {
        
        Collider.enabled = value;
        
    }
}

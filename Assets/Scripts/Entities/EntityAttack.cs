using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityAttack : MonoBehaviour
{
    
    [SerializeField] protected float coolDown;
    [SerializeField] EntityWeaponColliderScript weaponColliderScript;
    
    protected float currentCoolDown;
    protected Animator animator;
    protected bool IsEnabled;

    protected virtual void Start()
    {
        currentCoolDown = 0;
        animator = GetComponent<Animator>();
        IsEnabled = false;
        
    }
    protected virtual void OnEnable()
    {
        EntitiesEvent.OnEntityReadyToAttack += Attack;
    }
    protected virtual void OnDisable()
    {
        EntitiesEvent.OnEntityReadyToAttack -= Attack;
    }
    protected virtual void Update()
    {
        
        currentCoolDown -= Time.deltaTime;
    }
    protected virtual void Attack(GameObject sender)
    {
        if (sender != gameObject) return;
        if(currentCoolDown <= 0)
        {
            
            animator.SetTrigger("Attack");
            currentCoolDown = coolDown;
        }
        
    }

    public void EnableWeapon()
    {

        weaponColliderScript.EnableWeapon();

    }

    public void DisableWeapon()
    {

        weaponColliderScript.DisableWeapon();

    }

}

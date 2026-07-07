using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityAttack : MonoBehaviour
{
    
    [SerializeField] float coolDown;
    [SerializeField] EntityWeaponColliderScript weaponCollider;
    float currentCoolDown;
    Animator animator;
    bool IsEnabled;

    private void Start()
    {
        currentCoolDown = coolDown;
        animator = GetComponent<Animator>();
        IsEnabled = false;
    }
    private void OnEnable()
    {
        EntitiesEvent.OnEntityReadyToAttack += Attack;
    }
    private void OnDisable()
    {
        EntitiesEvent.OnEntityReadyToAttack -= Attack;
    }
    private void Update()
    {
        
        currentCoolDown -= Time.deltaTime;
    }
    void Attack(LivingAbstractClass entity, GameObject sender)
    {
        if (sender != gameObject) return;
        if(currentCoolDown <= 0)
        {
            
            animator.SetTrigger("Attack");
            currentCoolDown = coolDown;
        }
        
    }

    public void EnableWeapon() => weaponCollider.SetWeapon(true);
    public void DisableWeapon() => weaponCollider.SetWeapon(false);





}

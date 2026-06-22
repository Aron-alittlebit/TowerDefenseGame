using System.Collections;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float coolDown;
    float currentCoolDown;
    Animator animator;

    private void Start()
    {
        currentCoolDown = coolDown;
        animator = GetComponent<Animator>();
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
        //Debug.Log(currentCoolDown);
        //Debug.Log(damage);
        currentCoolDown -= Time.deltaTime;
    }
    void Attack(LivingAbstractClass entity, GameObject sender)
    {
        if (sender != gameObject) return;
        if(currentCoolDown <= 0)
        {
            entity.TakeDamage(damage);
            animator.SetTrigger("Attack");
            currentCoolDown = coolDown;
        }
        
    }

    
}

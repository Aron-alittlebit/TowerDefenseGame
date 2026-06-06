using System.Collections;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] int damage;
    public float CoolDown;
    float currentCoolDown;

    private void Start()
    {
        currentCoolDown = CoolDown;
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
    void Attack(LivingAbstractClass entity)
    {
        if(currentCoolDown <= 0)
        {
            entity.TakeDamage(damage);
            currentCoolDown = CoolDown;
        }
        
    }

    
}

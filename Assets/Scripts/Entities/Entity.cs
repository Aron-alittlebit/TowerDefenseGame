using UnityEngine;

public class Entity : LivingAbstractClass
{
    Animator animator;
    [SerializeField] GameObject GemPrefab;
    bool HasDied = false;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    public override void TakeDamage(int damage)
    {
        animator.SetTrigger("TakeDamage");
        health -= damage;
        Die();
    }

    protected override void Die()
    {
        if(health <= 0 && !HasDied)
        {
            animator.SetTrigger("Death");
            EntitiesEvent.EntityDeath(transform.GetInstanceID());
            GameObject gem = Instantiate(GemPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = gem.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(500f, transform.position, 5f);
                
            }
            HasDied = true;
            Destroy(gameObject);
        }
    }
    
}

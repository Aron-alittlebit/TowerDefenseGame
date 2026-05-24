using UnityEngine;

public class Entity : LivingAbstractClass
{
    
    [SerializeField] GameObject GemPrefab;
    bool HasDied = false;
    public override void TakeDamage(int damage)
    {
        health -= damage;
        Die();
    }

    protected override void Die()
    {
        if(health <= 0 && !HasDied)
        {
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

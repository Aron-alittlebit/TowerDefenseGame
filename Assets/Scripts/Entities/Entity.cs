using UnityEngine;

public class Entity : LivingAbstractClass
{
    
    [SerializeField] GameObject GemPrefab;
    public override void TakeDamage(int damage)
    {
        health -= damage;
        Die();
    }

    protected override void Die()
    {
        if(health <= 0)
        {
            EntitiesEvent.EntityDeath(transform.GetInstanceID());
            GameObject gem = Instantiate(GemPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = gem.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(500f, transform.position, 5f);
                
            }
            Destroy(gameObject);
        }
    }
    
}

using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int health;
    public int Health => health;
    [SerializeField] GameObject GemPrefab;
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Die();
    }

    private void Die()
    {
        if(health <= 0)
        {
            EntitiesEvent.EntityDeath(transform.name);
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

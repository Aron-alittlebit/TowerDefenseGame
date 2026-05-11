using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int health;
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

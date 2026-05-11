using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int health;
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Die();
    }

    private void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}

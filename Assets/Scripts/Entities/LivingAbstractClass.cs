using UnityEngine;

public abstract class LivingAbstractClass : MonoBehaviour
{
    [SerializeField] protected int StartingHealth;
    protected int health;
    //public int Health => health;

    protected virtual void Start()
    {
        health = StartingHealth;
    }

    

    public virtual void TakeDamage(int damage)
    {
        
        health -= damage;
        Die();
    }

    protected virtual void Die()
    {
        if (health <= 0)
        {
               
            Destroy(gameObject);
        }

    }

}

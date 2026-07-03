using System.Collections;
using UnityEngine;

public class Entity : LivingAbstractClass
{
    Animator animator;
    [SerializeField] GameObject GemPrefab;
    public bool HasDied { get; private set; } = false;
    bool Revived = false;
    Coroutine DeathCoroutine;

    protected override void Start()
    {
        base.Start();
        Revived = false;
        animator = GetComponent<Animator>();
    }
    public override void TakeDamage(int damage)
    {
        if (health <= 0) return;
        animator.SetTrigger("TakeDamage");
        health -= damage;
        Die();
    }

    protected override void Die()
    {
        if(health <= 0 && !HasDied)
        {
            EntitiesEvent.EntityDeath(gameObject.GetInstanceID());
            animator.SetBool("Walk", false);
            DeathCoroutine = StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        animator.SetTrigger("Death");
        CapsuleCollider[] colliders = GetComponents<CapsuleCollider>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
        
        GameObject gem = Instantiate(GemPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = gem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(500f, transform.position, 5f);

        }
        HasDied = true;
        yield return new WaitForSeconds(20);

        if (!Revived)
            Destroy(gameObject);
        
    }

    public void ComingBackFromDeath()
    {
        if(DeathCoroutine != null)
        {
            StopCoroutine(DeathCoroutine);
            DeathCoroutine = null;
        }
        Revived = true;
        HasDied = false;
        health = StartingHealth;
        CapsuleCollider[] colliders = GetComponents<CapsuleCollider>();
        foreach (var col in colliders)
        {
            col.enabled = true;
        }

        animator.ResetTrigger("Death");
        
        EntitiesEvent.ReviveEntity(gameObject.GetInstanceID());
        animator.SetTrigger("Resurrection");
        
        
    }
    
}

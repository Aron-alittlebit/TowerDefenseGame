using System.Collections;
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
        if (health <= 0) return;
        animator.SetTrigger("TakeDamage");
        health -= damage;
        Die();
    }

    protected override void Die()
    {
        if(health <= 0 && !HasDied)
        {
            animator.SetBool("Walk", false);
            StartCoroutine(DeathAnimation());
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
        EntitiesEvent.EntityDeath(transform.GetInstanceID());
        GameObject gem = Instantiate(GemPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = gem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(500f, transform.position, 5f);

        }
        HasDied = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    
}

using System.Collections;
using UnityEngine;

public class Entity : LivingAbstractClass
{
    Animator animator;
    [SerializeField] GameObject GemPrefab;
    public bool HasDied { get; private set; }
    bool Revived = false;
    Coroutine DeathCoroutine;
    [SerializeField] AudioClip DeathSound;

    protected override void Start()
    {
        base.Start();
        HasDied = false;
        Revived = false;
        animator = GetComponent<Animator>();
    }
    public override void TakeDamage(int damage)
    {
        if (health <= 0) return;
        hasFiredFinishedDamageAnim = false;
        animator.SetTrigger("TakeDamage");
        health -= damage;
        Die();
    }
    

    bool hasFiredFinishedDamageAnim = false;
    protected override void Die()
    {
        if(health <= 0 && !HasDied)
        {
            HasDied = true;
            Revived = false;
            SpawnEntities.NumberOfAllEntities--;
            EntitiesEvent.EntityDeath(gameObject.GetInstanceID());
            animator.SetBool("Walk", false);
            DeathCoroutine = StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        animator.SetTrigger("Death");
        SoundManager.instance.PlaySound(DeathSound, transform, 50f);
        CapsuleCollider[] colliders = GetComponents<CapsuleCollider>();
        foreach (var col in colliders)
        {
            if(!col.isTrigger)
                col.enabled = false;
        }
        
        GameObject gem = Instantiate(GemPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = gem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(500f, transform.position, 5f);

        }
        
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
        SpawnEntities.NumberOfAllEntities++;
        health = StartingHealth;
        CapsuleCollider[] colliders = GetComponents<CapsuleCollider>();
        foreach (var col in colliders)
        {
            if(!col.isTrigger)
                col.enabled = true;
        }
        
        animator.ResetTrigger("Death");
        
        EntitiesEvent.ReviveEntity(gameObject.GetInstanceID());
        animator.SetTrigger("Resurrection");
        
        
    }
    
}

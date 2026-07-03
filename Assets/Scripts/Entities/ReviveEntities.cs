using UnityEngine;

public class ReviveEntities : MonoBehaviour
{
    [SerializeField] float CoolDown;
    float currentCoolDown;
    [SerializeField] int Range = 20;
    [SerializeField] LayerMask EntityMask;
    Animator animator;
    bool foundDead;
    

    private void Start()
    {
        foundDead = false;
        currentCoolDown = CoolDown;
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {
        currentCoolDown -= Time.deltaTime;
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range, EntityMask);
        
        if(currentCoolDown <= 0 && colliders.Length > 0)
        {
            
            animator.SetBool("Walk", false);
            
            foreach(var col in colliders)
            {
                Entity entity = col.GetComponent<Entity>();
                
                if (entity != null && entity.HasDied)
                {
                    entity.ComingBackFromDeath();
                    foundDead = true;
                }
            }

            if (foundDead)
            {
                animator.SetTrigger("Revive");

            }

            animator.SetBool("Walk", true);
            currentCoolDown = CoolDown;
            foundDead = false;
            
            
        }
    }
}

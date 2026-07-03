using UnityEngine;

public class ReviveEntities : MonoBehaviour
{
    [SerializeField] float CoolDown;
    float currentCoolDown;
    [SerializeField] int Range = 20;
    [SerializeField] LayerMask EntityMask;
    Animator animator;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range, EntityMask);
        
        if(currentCoolDown <= 0 && colliders.Length > 0)
        {
            
            animator.SetBool("Walk", false);
            animator.SetTrigger("Revive");
            foreach(var col in colliders)
            {
                Entity entity = col.GetComponent<Entity>();
                if (entity != null && entity.HasDied)
                {
                    entity.ComingBackFromDeath();
                }
            }

            animator.SetBool("Walk", true);
            
            
        }
    }
}
